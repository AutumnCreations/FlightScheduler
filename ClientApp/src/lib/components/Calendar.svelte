<script lang="ts">
	import { createEventDispatcher, tick } from 'svelte';
	import { popup } from '@skeletonlabs/skeleton';
	import type { CalendarDay, Flight } from '$lib/types/types';
	import { fetchFlightPrices } from '$lib/types/api';
	import { onMount } from 'svelte';
	import { writable } from 'svelte/store';
	import { animateOnLoad, isToday, isSameDate, formatTime, getPopupSettings } from '$lib/index';

	export let calendarDays: CalendarDay[];
	export let weekdays: string[];
	export let cheapestPrice: number;
	export let view: 'month' | 'week';
	export let selectedDate: Date | null;

	let isCalendarReady = false;

	const animationDelay = 50;
	const dispatch = createEventDispatcher();
	const flightInfo = writable<Record<string, DayFlightInfo>>({});

	$: currentMonth = calendarDays[0]?.date?.getMonth();
	$: currentYear = calendarDays[0]?.date?.getFullYear();
	$: animationKey = `${view}-${currentYear}-${currentMonth}`;

	// Replay calendar animations when view, month, or year changes
	$: {
		if (view || currentMonth || currentYear) {
			isCalendarReady = false;
			setTimeout(async () => {
				await tick();
				isCalendarReady = true;
			}, 0);
		}
	}

	type DayFlightInfo = {
		count: number;
		firstFlight?: Flight;
		lastFlight?: Flight;
	};

	onMount(() => {
		isCalendarReady = true;
	});

	// Used for popup info display
	async function fetchDayFlightInfo(date: Date): Promise<DayFlightInfo> {
		const dateString = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
		if (!$flightInfo[dateString]) {
			const flights: Flight[] = await fetchFlightPrices(
				`/api/FlightPrices/daily?year=${date.getFullYear()}&month=${date.getMonth() + 1}&day=${date.getDate()}`
			);
			const info: DayFlightInfo = {
				count: flights.length,
				firstFlight: flights[0],
				lastFlight: flights[flights.length - 1]
			};
			flightInfo.update((currentInfo) => ({ ...currentInfo, [dateString]: info }));
		}
		return $flightInfo[dateString];
	}

	function handleDateClick(date: Date | null) {
		if (date) {
			dispatch('selectDate', date);
			const flightTable = document.querySelector('.flight-table');
			if (flightTable) {
				flightTable.scrollIntoView({ behavior: 'smooth' });
			}
		}
	}

	async function formatPopupContent(day: CalendarDay) {
		if (!day.date || !day.flight) return '';
		const info = await fetchDayFlightInfo(day.date);

		let content = `${info.count} flight${info.count !== 1 ? 's' : ''}<br>`;

		if (info.firstFlight) {
			content += `First: ${formatTime(info.firstFlight.departureTime)}<br>`;
		}

		if (info.lastFlight) {
			content += `Last: ${formatTime(info.lastFlight.departureTime)}<br>`;
		}

		return content;
	}

	function formatDate(date: Date | null): string {
		return date ? `${date.getDate()}` : '';
	}

	function formatPrice(price?: number): string {
		return price ? `$${Math.round(price)}` : '';
	}
</script>

<div class="weekdays">
	{#each weekdays as weekday}
		<div class="hidden md:block">
			{weekday}
		</div>
		<div class="block md:hidden">
			{weekday.slice(0, 2)}
		</div>
	{/each}
</div>
{#key animationKey}
	{#if isCalendarReady}
		<div class="calendar {view}">
			{#each calendarDays as day, i (day.date?.toISOString() || i)}
				{#if day.date}
					<button
						use:popup={getPopupSettings(`popup-${i}`)}
						use:animateOnLoad={{ delay: i * animationDelay, key: animationKey }}
						on:click={() => handleDateClick(day.date)}
						on:mouseenter={() => day.date && fetchDayFlightInfo(day.date)}
						disabled={!day.flight}
						class="card day [&>*]:pointer-events-none {day.flight
							? 'valid card-hover'
							: 'noFlight'} 
                            {day.flight &&
						Math.round(day.flight.price) === Math.round(cheapestPrice)
							? 'cheapest'
							: ''} 
                            {isToday(day.date) ? 'today' : ''}
                            {isSameDate(day.date, selectedDate) ? 'selected' : ''}"
					>
						<p class="dateBadge">
							{formatDate(day.date)}
						</p>
						<div class="flightEntry">
							{#if day.flight}
								<i
									class="fa-solid fa-tag scale-0 md:scale-100 {Math.round(day.flight.price) ===
									Math.round(cheapestPrice)
										? ''
										: 'opacity-0'}"
								></i>
								<p class="price">{formatPrice(day.flight.price)}</p>
							{:else}
								<div class="hidden lg:block">No Flights</div>
								<div class="block lg:hidden">-</div>
							{/if}
						</div>
					</button>
					{#if day.flight}
						<div
							use:animateOnLoad={{ delay: i * animationDelay, key: animationKey }}
							class="card p-2 shadow-xl variant-filled-secondary z-10 opacity-0 invisible transition-opacity duration-75 ease-out [&>*]:pointer-events-none"
							data-popup="popup-{i}"
						>
							{#await formatPopupContent(day)}
								Loading...
							{:then content}
								{@html content}
							{:catch error}
								Error loading flight information
							{/await}
							<div class="arrow bg-surface-100-800-token" />
						</div>
					{/if}
				{:else}
					<div class="day emptyDate"></div>
				{/if}
			{/each}
		</div>
	{/if}
{/key}
