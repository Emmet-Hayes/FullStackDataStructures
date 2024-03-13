<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <circle v-for="(arraynode, index) in arrayNodes" :key="'arraynode'+index"
                :cx="arraynode.x" :cy="arraynode.y" :r="radius"
                style="fill: lightblue; stroke: black; stroke-width: 1px;" />
        <text v-for="(arraynode, index) in arrayNodes" :key="'label'+index"
              :x="arraynode.x" :y="arraynode.y - 10" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ arraynode.label }}
        </text>
    </svg>
</template>

<script lang="ts">
    import { defineComponent, onMounted, PropType } from 'vue';

    interface ArrayNode {
        id: number;
        arrayid: number;
        index: number;
        label: string;
        x: number;
        y: number;
    }

    export default defineComponent({
        name: 'ArrayVisualizer',
        props: {
            arrayNodes: Array as PropType<ArrayNode[]>,
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


