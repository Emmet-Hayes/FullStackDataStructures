<script setup>
    import { onMounted, ref } from 'vue';
    import GraphVisualizer from './components/GraphVisualizer.vue';

    const graphData = ref({ nodes: [], edges: [] });

    onMounted(async () => {
        const response = await fetch('/Graph'); // Adjust the endpoint as necessary
        if (response.ok)
        {
            const data = await response.json();
            graphData.value = data;
        }
    });
</script>

<template>
    <main>
        <GraphVisualizer :nodes="graphData.nodes" :edges="graphData.edges" :isDirected="graphData.isDirected" />
    </main>
</template>

<style scoped>
    header {
        line-height: 1.5;
    }

    .logo {
        display: block;
        margin: 0 auto 2rem;
    }

    @media (min-width: 1024px) {
        header {
            display: flex;
            place-items: center;
            padding-right: calc(var(--section-gap) / 2);
        }

        .logo {
            margin: 0 2rem 0 0;
        }

        header .wrapper {
            display: flex;
            place-items: flex-start;
            flex-wrap: wrap;
        }
    }
</style>
