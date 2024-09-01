<script lang="ts">
	import '../app.postcss';
	import { AppShell, AppBar } from '@skeletonlabs/skeleton';
	import { LightSwitch } from '@skeletonlabs/skeleton';
	import '@fortawesome/fontawesome-free/css/all.min.css';
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
				<!-- 				<a
					class="btn btn-sm variant-ghost-surface"
					href="link"
					target="_blank"
					rel="noreferrer"
				>
					Text
				</a> -->
				<LightSwitch />
			</svelte:fragment>
		</AppBar>
	</svelte:fragment>
	<!-- Page Route Content -->
	<slot />
</AppShell>
