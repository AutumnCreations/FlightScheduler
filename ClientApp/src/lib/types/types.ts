export enum CalendarView {
	Month,
	Week,
	Day
}

export interface Airport {
	id: number;
	name: string;
	abbreviation: string;
	city: string;
	state: string;
}

export interface Flight {
	id: number;
	departureTime: string;
	arrivalTime: string;
	price: number;
	departureAirport: Airport;
	arrivalAirport: Airport;
}

export type CalendarDay = {
	date: Date | null;
	flight?: Flight;
	flights?: Number;
};
