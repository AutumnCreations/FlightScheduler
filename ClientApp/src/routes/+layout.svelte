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
				<strong class="text-xl w-44 md:w-full"
					>Flight Scheduler Simulator
					<i class="fas fa-plane-departure mx-0 md:mx-1"></i>
				</strong>
			</svelte:fragment>
			<svelte:fragment slot="trail">
				<a
					class="text-xl"
					href="https://github.com/AutumnCreations/FlightScheduler"
					target="_blank"
				>
					<i class="fa-brands fa-github"></i>
				</a>
				<LightSwitch />
			</svelte:fragment>
		</AppBar>
	</svelte:fragment>
	<!-- Page Route Content -->
	<slot />
</AppShell>
