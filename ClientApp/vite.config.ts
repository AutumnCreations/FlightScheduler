import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
		proxy: {
			'/api': 'http://localhost:5145',
		},
	},
	resolve: {
		alias: {
			'@skeletonlabs/tw-plugin': 'node_modules/@skeletonlabs/tw-plugin',
			'rollup': 'node_modules/rollup',
			'esbuild': 'node_modules/esbuild',
			'postcss': 'node_modules/postcss',
			'tailwindcss': 'node_modules/tailwindcss',
		},
	},
});
