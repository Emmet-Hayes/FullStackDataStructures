<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <circle v-for="(dictnode, index) in dictnodes" :key="'dictnode'+index"
                :cx="dictnode.x" :cy="dictnode.y" :r="radius"
                style="fill: lightblue; stroke: black; stroke-width: 1px;" />
        <text v-for="(dictnode, index) in dictnodes" :key="'label'+index"
              :x="dictnode.x" :y="dictnode.y - 10" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ dictnode.label }}
        </text>
    </svg>
</template>

<script lang="ts">
    import { defineComponent, onMounted, PropType } from 'vue';

    interface DictNode {
        id: number;
        listid: number;
        nextid: number; // Optional next ID
        label: string;
        x: number;
        y: number;
    }

    interface Edge {
        from: number; // Assuming these are indexes in the vertices array
        to: number;
    }

    export default defineComponent({
        name: 'DictVisualizer',
        props: {
            dictnodes: Array as PropType<DictNode[]>,
            width: {
                type: Number,
                default: 1200
            },
            height: {
                type: Number,
                default: 900
            },
            radius: {
                type: Number,
                default: 20
            },
            isBst: {
                type: Boolean,
                default: false
            },
            isBalanced: {
                type: Boolean,
                default: false
            }
        }
    });

</script>


