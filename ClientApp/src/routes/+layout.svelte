<script lang="ts">
	import '../app.postcss';
	import { AppShell, AppBar, LightSwitch } from '@skeletonlabs/skeleton';
	import '@fortawesome/fontawesome-free/css/all.min.css';
	import { computePosition, autoUpdate, offset, shift, flip, arrow } from '@floating-ui/dom';
	import { storePopup } from '@skeletonlabs/skeleton';

	storePopup.set({ computePosition, autoUpdate, offset, shift, flip, arrow });
</script>

<AppShell>
	<svelte:fragment slot="header">
		<AppBar>
			<svelte:fragment slot="lead">
				<strong class="text-xl w-44 md:w-full" aria-label="Flight Scheduler Simulator"
					>Flight Scheduler Simulator
					<i class="fas fa-plane-departure mx-0 md:mx-1" aria-hidden="true"></i>
				</strong>
			</svelte:fragment>
			<svelte:fragment slot="trail">
				<a
					class="text-xl"
					href="https://github.com/AutumnCreations/FlightScheduler"
					target="_blank"
					rel="noopener noreferrer"
					aria-label="View project on GitHub"
				>
					<i class="fa-brands fa-github" aria-hidden="true"></i>
				</a>
				<LightSwitch aria-label="Toggle light/dark mode" />
			</svelte:fragment>
		</AppBar>
	</svelte:fragment>
	<slot />
</AppShell>
