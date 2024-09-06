import { browser } from '$app/environment';
import type { PopupSettings } from '@skeletonlabs/skeleton';

export interface AnimateOptions {
	delay?: number;
	duration?: number;
	easing?: string;
	key?: string | number;
}

export function animateOnLoad(node: HTMLElement, options: AnimateOptions = {}) {
	const { delay = 0, duration = 700, easing = 'ease-in-out', key } = options;
	let currentKey = key;

	function setupAnimation() {
		if (browser) {
			node.style.opacity = '0';
			node.style.transform = 'translateY(0.75rem)';
			node.style.transition = `opacity ${duration}ms ${easing}, transform ${duration}ms ${easing}`;
			node.style.visibility = 'hidden';

			const animate = () => {
				const animationKey = currentKey;
				setTimeout(() => {
					if (animationKey === currentKey) {
						requestAnimationFrame(() => {
							node.style.visibility = 'visible';
							requestAnimationFrame(() => {
								node.style.opacity = '1';
								node.style.transform = 'translateY(0)';
							});
						});
					}
				}, delay);
			};

			animate();
		}
	}

	setupAnimation();

	return {
		update(newOptions: AnimateOptions) {
			if (newOptions.key !== currentKey) {
				currentKey = newOptions.key;
				setupAnimation();
			}
		},
		destroy() {}
	};
}

export function isToday(date: Date | null): boolean {
	if (!date) return false;
	const today = new Date();
	return (
		date.getDate() === today.getDate() &&
		date.getMonth() === today.getMonth() &&
		date.getFullYear() === today.getFullYear()
	);
}

export function isSameDate(date1: Date | null, date2: Date | null): boolean {
	if (!date1 || !date2) return false;
	return (
		date1.getDate() === date2.getDate() &&
		date1.getMonth() === date2.getMonth() &&
		date1.getFullYear() === date2.getFullYear()
	);
}

export function formatTime(date: string): string {
	return new Date(date).toLocaleTimeString([], {
		hour: 'numeric',
		minute: '2-digit',
		hour12: true
	});
}

let activePopup: string | null = null;

export function getPopupSettings(target: string): PopupSettings {
	return {
		event: 'hover',
		target: target,
		placement: 'top',
		state: (event) => {
			const popupElement = document.querySelector(`[data-popup="${target}"]`) as HTMLElement;
			if (popupElement) {
				if (event.state) {
					if (activePopup && activePopup !== target) {
						const prevPopup = document.querySelector(
							`[data-popup="${activePopup}"]`
						) as HTMLElement;
						if (prevPopup) {
							prevPopup.style.opacity = '0';
							prevPopup.style.visibility = 'hidden';
						}
					}
					activePopup = target;
					setTimeout(() => {
						popupElement.style.opacity = '1';
						popupElement.style.visibility = 'visible';
					}, 10);
				} else {
					popupElement.style.opacity = '0';
					popupElement.style.visibility = 'hidden';
					if (activePopup === target) {
						activePopup = null;
					}
				}
			}
		}
	};
}
