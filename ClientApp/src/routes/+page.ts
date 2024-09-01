import type { LoadEvent } from '@sveltejs/kit';

export const load = async ({ fetch }: LoadEvent) => {
	const response = await fetch('/api/FlightPrices');
	if (response.ok) {
		const flightPrices = await response.json();
		return { flightPrices };
	} else {
		throw new Error('Failed to fetch flight prices');
	}
};