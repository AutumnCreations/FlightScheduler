export async function fetchFlightPrices(url: string) {
	const response = await fetch(url);
	if (response.ok) {
		return await response.json();
	} else {
		console.error('Failed to fetch flight prices');
		return [];
	}
}
