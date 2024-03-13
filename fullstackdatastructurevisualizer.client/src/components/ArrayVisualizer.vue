<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <!-- Draw squares for arrayNodes -->
        <g v-for="(arraynode, index) in arrayNodes" :key="'arraynode'+index">
            <rect :x="arraynode.x" :y="arraynode.y" :width="size" :height="size"
                  style="fill: lightcoral; stroke: black; stroke-width: 1px;" />
            <!-- Label text -->
            <text :x="arraynode.x + size/2" :y="arraynode.y - 25" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 12px; user-select: none;">
                {{ arraynode.label }}
            </text>
            <!-- Value text (optional: adjust positioning as needed) -->
            <text :x="arraynode.x + size/2" :y="arraynode.y + 25" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 10px; user-select: none;">
                {{ arraynode.value }}
            </text>
        </g>
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
        value: number;
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
            size: {
                type: Number,
                default: 50 // Size of the square
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


