<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	import { popup } from '@skeletonlabs/skeleton';
	import type { PopupSettings } from '@skeletonlabs/skeleton';
	import type { CalendarDay, Flight } from '$lib/types/types';

	export let calendarDays: CalendarDay[];
	export let weekdays: string[];
	export let cheapestPrice: number;
	export let formatDate: (date: Date | null) => string;
	export let formatPrice: (price?: number) => string;
	export let view: 'month' | 'week';
	export let selectedDate: Date | null;

	const dispatch = createEventDispatcher();

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

	function handleDateClick(date: Date | null) {
		if (date) {
			dispatch('selectDate', date);
			const flightTable = document.querySelector('.flight-table');
			if (flightTable) {
				flightTable.scrollIntoView({ behavior: 'smooth' });
			}
		}
	}

	function formatPopupContent(day: CalendarDay) {
		if (!day.date || !day.flight) return '';
		const dateString = day.date.toLocaleDateString();
		const departureTime = new Date(day.flight.departureTime).toLocaleTimeString([], {
			hour: '2-digit',
			minute: '2-digit'
		});
		const arrivalTime = new Date(day.flight.arrivalTime).toLocaleTimeString([], {
			hour: '2-digit',
			minute: '2-digit'
		});
		return `${dateString}<br>Departure: ${departureTime}<br>Arrival: ${arrivalTime}<br>Price: ${formatPrice(day.flight.price)}`;
	}

	function getPopupSettings(target: string): PopupSettings {
		return {
			event: 'hover',
			target: target,
			placement: 'top',
			closeQuery: '.closest-remove'
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
				disabled={!day.flight}
				class="card day {day.flight ? 'valid card-hover' : 'noFlight'} 
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
				<div class="card p-2 shadow-xl" data-popup="popup-{i}">
					{@html formatPopupContent(day)}
					<div class="arrow bg-surface-100-800-token" />
				</div>
			{/if}
		{:else}
			<div class="day emptyDate"></div>
		{/if}
	{/each}
</div>
