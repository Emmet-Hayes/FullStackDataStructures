<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <!-- Draw squares for dictNodes -->
        <g v-for="(dictnode, index) in dictNodes" :key="'dictnode'+index">
            <!-- Rectangle for the key (left half) -->
            <rect :x="dictnode.x" :y="dictnode.y" :width="size" :height="size/2"
                  style="fill: lightcoral; stroke: black; stroke-width: 1px;" />
            <!-- Rectangle for the value (right half) -->
            <rect :x="dictnode.x" :y="dictnode.y - size/2" :width="size" :height="size/2"
                  style="fill: lightblue; stroke: black; stroke-width: 1px;" />
            <!-- Label text -->
            <text :x="dictnode.x + size/2" :y="dictnode.y - 50" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 12px; user-select: none;">
                {{ dictnode.label }}
            </text>
            <!-- Key text (optional: adjust positioning as needed) -->
            <text :x="dictnode.x + size/2" :y="dictnode.y - 25" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 10px; user-select: none;">
                {{ dictnode.key }}
            </text>
            <!-- Value text (optional: adjust positioning as needed) -->
            <text :x="dictnode.x + size/2" :y="dictnode.y + 25" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 10px; user-select: none;">
                {{ dictnode.value }}
            </text>
        </g>
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
            dictNodes: Array as PropType<DictNode[]>,
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
                default: 80 // Size of the square
            },
            // Removed radius since we are using squares now
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


