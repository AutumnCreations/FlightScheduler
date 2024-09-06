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

<div class="daily-view" use:animateOnLoad={{ key: animationKey }}>
	<h3>Available flights for {selectedDate.toDateString()}</h3>
	{#if flights.length > 0}
		<table class="flight-table">
			<thead>
				<tr>
					<th>
						<div class="md:hidden block"><i class="fas fa-plane-departure"></i></div>
						<div class="hidden md:block">Departure</div>
					</th>
					<th>
						<div class="md:hidden block"><i class="fas fa-plane-arrival"></i></div>
						<div class="hidden md:block">Arrival</div>
					</th>
					<th>
						<div class="md:hidden block"><i class="fas fa-clock"></i></div>
						<div class="hidden md:block">Duration</div>
					</th>
					<th>
						<div class="md:hidden block"><i class="fas fa-dollar"></i></div>
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
							{formatTime(flight.departureTime)}
							<i class="fas fa-plane-departure table-icon"></i><strong
								>{flight.departureAirport.abbreviation}</strong
							>
						</td>
						<td
							use:animateOnLoad={{ delay: i * animationDelay, key: `${animationKey}-${i}-arrival` }}
						>
							{formatTime(flight.arrivalTime)}
							<i class="fas fa-plane-arrival table-icon"></i><strong
								>{flight.arrivalAirport.abbreviation}</strong
							>
						</td>
						<td
							use:animateOnLoad={{
								delay: i * animationDelay,
								key: `${animationKey}-${i}-duration`
							}}
						>
							{formatDuration(flight.departureTime, flight.arrivalTime)}
							<i class="fas fa-clock table-icon"></i>
						</td>
						<td
							use:animateOnLoad={{ delay: i * animationDelay, key: `${animationKey}-${i}-price` }}
						>
							${flight.price.toFixed(2)}
							{#if flights.length > 1 && flight.price === Math.min(...flights.map((f) => f.price))}
								<i class="fas fa-tag cheapest ml-1"></i>
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
