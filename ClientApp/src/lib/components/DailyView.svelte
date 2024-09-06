<script lang="ts">
	import type { Flight } from '$lib/types/types';
	import { animateOnLoad, formatTime } from '$lib/index';

	export let selectedDate: Date;
	export let flights: Flight[];

	const animationDelay = 50;

	function formatDuration(departureTime: string, arrivalTime: string): string {
		const departure = new Date(departureTime);
		const arrival = new Date(arrivalTime);
		const durationMs = arrival.getTime() - departure.getTime();
		const hours = Math.floor(durationMs / (1000 * 60 * 60));
		const minutes = Math.floor((durationMs % (1000 * 60 * 60)) / (1000 * 60));
		return `${hours}h ${minutes > 0 ? minutes + 'm' : ''}`;
	}

	$: animationKey = selectedDate.toISOString();
</script>

<div
	class="daily-view"
	use:animateOnLoad={{ key: animationKey }}
	role="region"
	aria-label="Available flights for {selectedDate.toDateString()}"
>
	<h3>Available flights for {selectedDate.toDateString()}</h3>
	{#if flights.length > 0}
		<table class="flight-table" aria-labelledby="flights-heading">
			<thead>
				<tr>
					<th scope="col">
						<div class="md:hidden block" aria-hidden="true">
							<i class="fas fa-plane-departure"></i>
						</div>
						<div class="hidden md:block">Departure</div>
					</th>
					<th scope="col">
						<div class="md:hidden block" aria-hidden="true">
							<i class="fas fa-plane-arrival"></i>
						</div>
						<div class="hidden md:block">Arrival</div>
					</th>
					<th scope="col">
						<div class="md:hidden block" aria-hidden="true">
							<i class="fas fa-clock"></i>
						</div>
						<div class="hidden md:block">Duration</div>
					</th>
					<th scope="col">
						<div class="md:hidden block" aria-hidden="true">
							<i class="fas fa-dollar"></i>
						</div>
						<div class="hidden md:block">Price</div>
					</th>
				</tr>
			</thead>
			<tbody>
				{#each flights as flight, i}
					<tr>
						<td
							use:animateOnLoad={{
								delay: i * animationDelay,
								key: `${animationKey}-${i}-departure`
							}}
						>
							<span class="sr-only">Departure:</span>
							{formatTime(flight.departureTime)}
							<i class="fas fa-plane-departure table-icon" aria-hidden="true"></i>
							<span class="sr-only">{flight.departureAirport.name}</span>
							<strong>{flight.departureAirport.abbreviation}</strong>
						</td>
						<td
							use:animateOnLoad={{
								delay: i * animationDelay,
								key: `${animationKey}-${i}-arrival`
							}}
						>
							<span class="sr-only">Arrival:</span>
							{formatTime(flight.arrivalTime)}
							<i class="fas fa-plane-arrival table-icon" aria-hidden="true"></i>
							<span class="sr-only">{flight.arrivalAirport.name}</span>
							<strong>{flight.arrivalAirport.abbreviation}</strong>
						</td>
						<td
							use:animateOnLoad={{
								delay: i * animationDelay,
								key: `${animationKey}-${i}-duration`
							}}
						>
							<span class="sr-only">Duration:</span>
							{formatDuration(flight.departureTime, flight.arrivalTime)}
							<i class="fas fa-clock table-icon" aria-hidden="true"></i>
						</td>
						<td
							use:animateOnLoad={{
								delay: i * animationDelay,
								key: `${animationKey}-${i}-price`
							}}
						>
							<span class="sr-only">Price:</span>
							${flight.price.toFixed(2)}
							{#if flights.length > 1 && flight.price === Math.min(...flights.map((f) => f.price))}
								<span class="sr-only">Cheapest flight for this day</span>
								<i class="fas fa-tag cheapest ml-1" aria-hidden="true"></i>
							{/if}
						</td>
					</tr>
				{/each}
			</tbody>
		</table>
	{:else}
		<p>No flights available for this date.</p>
	{/if}
</div>
