<script lang="ts">
	import { onMount, tick } from 'svelte';
	import { writable } from 'svelte/store';
	import { CalendarView } from '../lib/types/types';
	import { fetchFlightPrices } from '../lib/types/api';
	import Calendar from '../lib/components/Calendar.svelte';
	import DailyView from '../lib/components/DailyView.svelte';

	type FlightPrice = {
		departureTime: string;
		arrivalTime: string;
		price: number;
	};

	type CalendarDay = {
		date: Date | null;
		flight?: FlightPrice;
	};

	let flightPrices: FlightPrice[] = [];
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
	const calendarDays = writable<CalendarDay[]>([]);

	let year = today.getFullYear();
	let month = today.getMonth();
	let week = new Date(today.setDate(today.getDate() - today.getDay()));
	let selectedDate: Date | null = null;
	let view: CalendarView = CalendarView.Month;
	let cheapestPrice: number;
	let dailyFlights: FlightPrice[] = [];

	$: monthName = monthNames[month];
	$: daysInMonth = new Date(year, month + 1, 0).getDate();
	$: titleDate = getTitleDate();
	$: {
		if (view !== CalendarView.Day || flightPrices.length === 0) {
			cheapestPrice = Math.min(...flightPrices.map((fp) => fp.price));
		}
	}

	onMount(async () => {
		await updateView();
	});

	async function fetchMonthlyPrices() {
		flightPrices = await fetchFlightPrices(
			`/api/FlightPrices/monthly?year=${year}&month=${month + 1}`
		);
		buildMonthCalendar();
	}

	async function fetchWeeklyPrices() {
		const endOfWeek = new Date(week);
		endOfWeek.setDate(endOfWeek.getDate() + 6);

		flightPrices = await fetchFlightPrices(
			`/api/FlightPrices/weekly?year=${year}&month=${month + 1}&day=${week.getDate()}`
		);
		buildWeekCalendar(week, endOfWeek);
	}

	async function fetchDailyPrices() {
		if (selectedDate) {
			flightPrices = await fetchFlightPrices(
				`/api/FlightPrices/daily?year=${selectedDate.getFullYear()}&month=${selectedDate.getMonth() + 1}&day=${selectedDate.getDate()}`
			);
		}
	}

	function buildMonthCalendar() {
		const firstDay = new Date(year, month, 1);
		const startDay = firstDay.getDay();
		const daysArray: CalendarDay[] = [];

		for (let i = 0; i < startDay; i++) {
			daysArray.push({ date: null });
		}

		for (let day = 1; day <= daysInMonth; day++) {
			const date = new Date(year, month, day);
			const flight = flightPrices.find((fp) => new Date(fp.departureTime).getDate() === day);
			daysArray.push({ date, flight });
		}

		calendarDays.set(daysArray);
	}

	function buildWeekCalendar(startOfWeek: Date, endOfWeek: Date) {
		const daysArray: CalendarDay[] = [];
		let currentDate = new Date(startOfWeek);

		while (currentDate <= endOfWeek) {
			const flight = flightPrices.find(
				(fp) => new Date(fp.departureTime).toDateString() === currentDate.toDateString()
			);
			daysArray.push({ date: new Date(currentDate), flight });
			currentDate.setDate(currentDate.getDate() + 1);
		}

		calendarDays.set(daysArray);
	}

	function setView(newView: CalendarView) {
		view = newView;
		jumpToToday();
	}

	async function updateView() {
		switch (view) {
			case CalendarView.Month:
				await fetchMonthlyPrices();
				break;
			case CalendarView.Week:
				await fetchWeeklyPrices();
				break;
			case CalendarView.Day:
				await fetchDailyPrices();
				break;
		}
	}

	async function updateOffset(newOffset: number) {
		switch (view) {
			case CalendarView.Month:
				month += newOffset;
				if (month > 11) {
					year++;
					month = 0;
				} else if (month < 0) {
					year--;
					month = 11;
				}
				selectedDate = null;
				break;
			case CalendarView.Week:
				let currentDate = new Date(week);
				if (selectedDate) {
					currentDate = new Date(selectedDate);
				}
				let first = currentDate.getDate() - currentDate.getDay();
				week = new Date(currentDate.setDate(first + newOffset * 7));
				year = currentDate.getFullYear();
				month = currentDate.getMonth();
				selectedDate = null;
				break;
			case CalendarView.Day:
				if (selectedDate) {
					selectedDate = new Date(selectedDate.setDate(selectedDate.getDate() + newOffset));
				} else {
					let currentDate = new Date(year, month, 1);
					currentDate.setDate(currentDate.getDate() + newOffset);
					selectedDate = currentDate;
				}
				year = selectedDate.getFullYear();
				month = selectedDate.getMonth();
				break;
		}
		await tick();
		await updateView();
	}

	async function selectDate(date: Date | undefined) {
		if (date) {
			selectedDate = date;
			dailyFlights = await fetchFlightPrices(
				`/api/FlightPrices/daily?year=${date.getFullYear()}&month=${date.getMonth() + 1}&day=${date.getDate()}`
			);
		}
	}

	function getTitleDate() {
		switch (view) {
			case CalendarView.Day:
				return selectedDate ? selectedDate.toDateString() : new Date(year, month, 1).toDateString();
			case CalendarView.Week:
				const weekStart = new Date(year, month, selectedDate ? selectedDate.getDate() : 1);
				weekStart.setDate(weekStart.getDate() - weekStart.getDay());
				return `Week of ${weekStart.toDateString()}`;
			default:
				return `${monthName}, ${year}`;
		}
	}

	function formatDate(date: Date | null): string {
		return date ? `${date.getDate()}` : '';
	}

	function formatPrice(price?: number): string {
		return price !== undefined ? `$${Math.round(price)}` : '';
	}

	function jumpToToday() {
		year = today.getFullYear();
		month = today.getMonth();
		week = new Date(today.setDate(today.getDate() - today.getDay()));
		selectedDate = view === CalendarView.Day ? new Date() : null;
		updateView();
	}
</script>

<div class="container">
	<div class="flightScheduler">
		<div class="dateRange">
			<button type="button" class="jumpButton" on:click={jumpToToday}>
				<div class="hidden md:block">Jump to Today</div>
				<div class="block md:hidden">Today</div>
			</button>
			<div class="dateButtons">
				<button
					type="button"
					class="dateRangeBtn"
					class:active={view === CalendarView.Month}
					on:click={() => setView(CalendarView.Month)}
				>
					Month
				</button>
				<button
					type="button"
					class="dateRangeBtn"
					class:active={view === CalendarView.Week}
					on:click={() => setView(CalendarView.Week)}
				>
					Week
				</button>
				<button
					type="button"
					class="dateRangeBtn"
					class:active={view === CalendarView.Day}
					on:click={() => setView(CalendarView.Day)}
				>
					Day
				</button>
			</div>
		</div>
		<div class="flex items-center justify-center">
			<button type="button" class="iconBtn" on:click={() => updateOffset(-1)}>
				<i class="fa-solid fa-caret-left"></i>
			</button>
			<h2 class="h2 w-2/3 md:w-2/5">
				{#if view === CalendarView.Day}
					{selectedDate ? selectedDate.toDateString() : today.toDateString()}
				{:else if view === CalendarView.Week}
					Week of {week.toDateString()}
				{:else}
					{monthName}, {year}
				{/if}
			</h2>
			<button type="button" class="iconBtn" on:click={() => updateOffset(1)}>
				<i class="fa-solid fa-caret-right"></i>
			</button>
		</div>

		{#if view != CalendarView.Day}
			<Calendar
				calendarDays={$calendarDays}
				{weekdays}
				{cheapestPrice}
				{formatDate}
				{formatPrice}
				view={view === CalendarView.Month ? 'month' : 'week'}
				{selectedDate}
				on:selectDate={(event) => selectDate(event.detail)}
			/>
		{/if}

		{#if view === CalendarView.Day}
			<DailyView selectedDate={selectedDate || new Date(year, month, 1)} flights={flightPrices} />
		{:else if selectedDate && dailyFlights.length > 0}
			<DailyView {selectedDate} flights={dailyFlights} />
		{/if}
	</div>
</div>
