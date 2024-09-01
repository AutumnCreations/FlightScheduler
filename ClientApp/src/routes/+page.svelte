<script lang="ts">
	import { onMount, tick } from 'svelte';
	import { writable } from 'svelte/store';
	let flightPrices: Array<{ date: string; price: number }> = [];
	const monthNames = [
		'January',
		'February',
		'March',
		'April',
		'May',
		'June',
		'July',
		'August',
		'September',
		'October',
		'November',
		'December'
	];
	const weekdays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
	const today = new Date();
	const calendarDays = writable<Array<{ date: Date | null; price?: number }>>([]);

	let year = today.getFullYear();

	$: month = today.getMonth();
	$: monthName = monthNames[month];
	$: daysInMonth = new Date(year, month + 1, 0).getDate();

	$: cheapestPrice = Number.MAX_VALUE;

	const fetchFlightPrices = async () => {
		const response = await fetch(`/api/FlightPrices/monthly?year=${year}&month=${month}`);
		if (response.ok) {
			flightPrices = await response.json();
			buildCalendar();
		} else {
			console.error('Failed to fetch flight prices');
		}
	};

	onMount(async () => {
		await fetchFlightPrices();
	});

	const buildCalendar = () => {
		const firstDay = new Date(year, month, 1);
		const startDay = firstDay.getDay();
		const daysArray = [];

		for (let i = 0; i < startDay; i++) {
			daysArray.push({ date: null });
		}

		for (let day = 1; day <= daysInMonth; day++) {
			const date = new Date(year, month, day);
			let flight = null;

			if (flightPrices.length > 0) {
				const nextFlight = new Date(flightPrices[0].date);
				if (nextFlight.getDate() === day) {
					flight = flightPrices.shift();
				}
			}
			if (flight) {
				daysArray.push({ date, price: flight.price });
				cheapestPrice = Math.min(cheapestPrice, flight.price);
				flight = null;
			} else {
				daysArray.push({ date });
			}
		}
		// console.log(cheapestPrice);
		calendarDays.set(daysArray);
	};

	function formatDate(date: Date | null): string {
		return date ? `${date.getDate()}` : '';
	}

	function formatPrice(price?: number): string {
		return price !== undefined ? `$${Math.round(price)}` : '';
	}

	const updateMonthOffset = async function (newOffset: number) {
		month += newOffset;
		if (month > 11) {
			year++;
			month = 0;
		} else if (month < 0) {
			year--;
			month = 11;
		}
		cheapestPrice = Number.MAX_VALUE;
		await tick();
		await fetchFlightPrices();
	};
</script>

<div class="container">
	<div class="flightScheduler">
		<div class="dateRange">
			<button type="button" class="jumpButton"
				><div class="hidden md:block">Jump to Today</div>
				<div class="block md:hidden">Today</div>
			</button>
			<div class="dateButtons">
				<button type="button" class="customButton">Month</button>
				<button type="button" class="customButton">Week</button>
				<button type="button" class="customButton">Day</button>
			</div>
		</div>
		<div class=" flex items-center justify-center">
			<button type="button" class="iconBtn" on:click={() => updateMonthOffset(-1)}>
				<i class="fa-solid fa-caret-left"></i>
			</button>
			<h2 class="h2 w-2/3 md:w-2/5">{monthName}, {year}</h2>
			<button type="button" class="iconBtn" on:click={() => updateMonthOffset(1)}>
				<i class="fa-solid fa-caret-right"></i>
			</button>
		</div>
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
		<div class="calendar">
			{#each $calendarDays as day}
				{#if day.date && day.price !== undefined}
					<a
						href="#top"
						class="card card-hover day valid {Math.round(day.price) === Math.round(cheapestPrice)
							? 'cheapest'
							: ''}"
					>
						<p class="dateBadge">{formatDate(day.date)}</p>
						<div class="flightEntry">
							<i
								class="fa-solid fa-tag scale-0 md:scale-100 {Math.round(day.price) ===
								Math.round(cheapestPrice)
									? ''
									: 'opacity-0'}"
							></i>
							<p class="price">{formatPrice(day.price)}</p>
						</div>
					</a>
				{:else if day.date}
					<div class="card day noFlight">
						<p class="dateBadge">{formatDate(day.date)}</p>
						<div class="flightEntry">
							<div class="hidden lg:block">No Flights</div>
							<div class="block lg:hidden">-</div>
						</div>
					</div>
				{:else}
					<div class="day emptyDate"></div>
				{/if}
			{/each}
		</div>
	</div>
</div>
