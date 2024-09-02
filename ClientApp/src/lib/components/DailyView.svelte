<script lang="ts">
	type FlightPrice = {
		departureTime: string;
		arrivalTime: string;
		price: number;
	};

	export let selectedDate: Date;
	export let flights: FlightPrice[];

	function formatDateTime(dateTimeString: string): string {
		const date = new Date(dateTimeString);
		return date.toLocaleTimeString('en-US', {
			hour: 'numeric',
			minute: '2-digit',
			hour12: true
		});
	}

	function formatDuration(departureTime: string, arrivalTime: string): string {
		const departure = new Date(departureTime);
		const arrival = new Date(arrivalTime);
		const durationMs = arrival.getTime() - departure.getTime();
		const hours = Math.floor(durationMs / (1000 * 60 * 60));
		const minutes = Math.floor((durationMs % (1000 * 60 * 60)) / (1000 * 60));
		return `${hours}h ${minutes > 0 ? minutes + 'm' : ''}`;
	}
</script>

<div class="daily-view">
	<h3>Available flights for {selectedDate.toDateString()}</h3>
	{#if flights.length > 0}
		<table class="flight-table">
			<thead>
				<tr>
					<th
						><div class="md:hidden block"><i class="fas fa-plane-departure"></i></div>
						<div class="hidden md:block">Departure</div></th
					>
					<th
						><div class="md:hidden block"><i class="fas fa-plane-arrival"></i></div>
						<div class="hidden md:block">Arrival</div></th
					>
					<th
						><div class="md:hidden block"><i class="fas fa-clock"></i></div>
						<div class="hidden md:block">Duration</div></th
					>
					<th
						><div class="md:hidden block"><i class="fas fa-dollar"></i></div>
						<div class="hidden md:block">Price</div></th
					>
				</tr>
			</thead>
			<tbody>
				{#each flights as flight}
					<tr>
						<td
							>{formatDateTime(flight.departureTime)}
							<i class="fas fa-plane-departure table-icon"></i>
						</td>
						<td
							>{formatDateTime(flight.arrivalTime)}
							<i class="fas fa-plane-arrival table-icon"></i>
						</td>
						<td
							>{formatDuration(flight.departureTime, flight.arrivalTime)}
							<i class="fas fa-clock table-icon"></i>
						</td>
						<td
							>${flight.price.toFixed(2)}
							{#if flights.length > 1 && flight.price === Math.min(...flights.map((f) => f.price))}
								<i class="fas fa-tag cheapest mx-2"></i>
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
