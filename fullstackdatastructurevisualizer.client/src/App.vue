<script setup>
    import { onMounted, ref } from 'vue';
    import GraphVisualizer from './components/GraphVisualizer.vue';
    import TreeVisualizer from './components/TreeVisualizer.vue';
    import ListVisualizer from './components/ListVisualizer.vue';
    /*
    import ArrayVisualizer from './components/ArrayVisualizer.vue';
    import DictVisualizer from './components/DictVisualizer.vue';
    */

    const graphData = ref({ vertices: [], edges: [] });

    const treeData = ref({ treeNodes: [] });
    /*
    const listData = ref({ listNodes: [] });

    
    const arrayData = ref({ arrayNodes: [] });

    const dictData = ref({ dictNodes: [] });
    */

    onMounted(async () => {
        const response = await fetch('/Graph'); // Adjust the endpoint as necessary
        if (response.ok)
        {
            const data = await response.json();
            graphData.value = data;
        }

        console.log("Trying to get the Tree");
        const response2 = await fetch('/Tree');
        if (response2.ok)
        {
            const data2 = await response2.json();
            treeData.value = data2;
            console.log("after the transfer");
        }
        /*
        console.log("Tring to get the list");
        const response3 = await fetch('/List');
        if (response3.ok)
        {
            console.log("Response is ok");
            const data3 = await response3.json();
            listData.value = data3;
        }
        
        console.log("Tring to get the array");
        const response4 = await fetch('/AArray');
        if (response4.ok) {
            console.log("Response is ok");
            const data4 = await response4.json();
            arrayData.value = data4;
        }

        console.log("Tring to get the dictionary");
        const response5 = await fetch('/DDict');
        if (response5.ok) {
            console.log("Response is ok");
            const data5 = await response5.json();
            dictData.value = data5;
        }*/
    });
</script>

<template>
    <main>
        <GraphVisualizer :vertices="graphData.vertices" :edges="graphData.edges" :isDirected="graphData.isDirected" />
        <br />
        <TreeVisualizer :treeNodes="treeData.treeNodes" :isBst="treeData.isBst" :isBalanced="treeData.isBalanced" />
        <br />
        <!--
        <ListVisualizer :listNodes="listData.listNodes" :isdoublylinked="listData.isDoublyLinked" />
        <br />
        <ArrayVisualizer :arrayNodes="arrayData.arrayNodes" :isdynamic="arrayData.isDynamic" />
        <br />
        <DictVisualizer :dictNodes="dictData.dictNodes" :hashfunction="dictData.hashfunction" />-->
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
