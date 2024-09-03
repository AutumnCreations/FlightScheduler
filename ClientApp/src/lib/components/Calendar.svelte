<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	import { popup } from '@skeletonlabs/skeleton';
	import type { PopupSettings } from '@skeletonlabs/skeleton';
	import type { CalendarDay, Flight } from '$lib/types/types';
	import { fetchFlightPrices } from '$lib/types/api';
	import { onMount } from 'svelte';
	import { writable } from 'svelte/store';

	export let calendarDays: CalendarDay[];
	export let weekdays: string[];
	export let cheapestPrice: number;
	export let formatDate: (date: Date | null) => string;
	export let formatPrice: (price?: number) => string;
	export let view: 'month' | 'week';
	export let selectedDate: Date | null;

	const dispatch = createEventDispatcher();
	const flightCounts = writable<Record<string, number>>({});
	const flightInfo = writable<Record<string, DayFlightInfo>>({});

	$: isToday = (date: Date | null) => {
		if (!date) return false;
		const today = new Date();
		return (
			date.getDate() === today.getDate() &&
			date.getMonth() === today.getMonth() &&
			date.getFullYear() === today.getFullYear()
		);
	};

	$: isSelected = (date: Date | null) => {
		if (!date || !selectedDate) return false;
		return (
			date.getDate() === selectedDate.getDate() &&
			date.getMonth() === selectedDate.getMonth() &&
			date.getFullYear() === selectedDate.getFullYear()
		);
	};

	type DayFlightInfo = {
		count: number;
		firstFlight?: Flight;
		lastFlight?: Flight;
	};

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
		const dateString = day.date.toLocaleDateString();
		const info = await fetchDayFlightInfo(day.date);

		const formatTime = (date: string) =>
			new Date(date).toLocaleTimeString([], {
				hour: 'numeric',
				minute: '2-digit',
				hour12: true
			});

		let content = `${info.count} flight${info.count !== 1 ? 's' : ''}<br>`;

		if (info.firstFlight) {
			content += `First: ${formatTime(info.firstFlight.departureTime)}<br>`;
		}

		if (info.lastFlight) {
			content += `Last: ${formatTime(info.lastFlight.departureTime)}<br>`;
		}

		return content;
	}

	function getPopupSettings(target: string): PopupSettings {
		return {
			event: 'hover',
			target: target,
			placement: 'top'
		};
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
<div class="calendar {view}">
	{#each calendarDays as day, i}
		{#if day.date}
			<button
				use:popup={getPopupSettings(`popup-${i}`)}
				on:click={() => handleDateClick(day.date)}
				on:mouseenter={() => day.date && fetchDayFlightInfo(day.date)}
				disabled={!day.flight}
				class="card day [&>*]:pointer-events-none {day.flight ? 'valid card-hover' : 'noFlight'} 
					{day.flight && Math.round(day.flight.price) === Math.round(cheapestPrice) ? 'cheapest' : ''} 
					{isToday(day.date) ? 'today' : ''}
					{isSelected(day.date) ? 'selected' : ''}"
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
				<div class="card p-2 shadow-xl variant-filled-secondary z-10" data-popup="popup-{i}">
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
