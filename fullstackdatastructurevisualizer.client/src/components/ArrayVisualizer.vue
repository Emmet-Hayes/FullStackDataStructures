<template>
    <svg :width="width" :height="height" style="border: 1px solid black;"
         @mousemove="mouseMove" @mouseup="mouseUp" @mouseleave="mouseUp">
        <g v-for="(arraynode, index) in arrayNodes" :key="'arraynode'+index">
            <rect :x="arraynode.x" :y="arraynode.y" :width="size" :height="size"
                  style="fill: lightcoral; stroke: black; stroke-width: 1px;"
                  @mousedown="mouseDown(index, $event)" />
            <text :x="arraynode.x + size/2" :y="arraynode.y - 25" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 16px; user-select: none;">
                {{ arraynode.label }}
            </text>
            <text :x="arraynode.x + size/2" :y="arraynode.y - 5" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 12px; user-select: none;">
                {{ arraynode.index }}
            </text>
            <text :x="arraynode.x + size/2" :y="arraynode.y + 25" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 12px; user-select: none;">
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
        },
        data() {
            return {
                dragging: false,
                draggedNodeIndex: null,
                startX: 0,
                startY: 0,
            };
        },
        methods: {
            mouseDown(index, event) {
                this.dragging = true;
                this.draggedNodeIndex = index;
                this.startX = event.clientX;
                this.startY = event.clientY;
            },
            mouseMove(event) {
                if (!this.dragging || this.draggedNodeIndex === null) return;

                const dx = event.clientX - this.startX;
                const dy = event.clientY - this.startY;

                this.arrayNodes[this.draggedNodeIndex].x += dx;
                this.arrayNodes[this.draggedNodeIndex].y += dy;

                // Update start position for next move event
                this.startX = event.clientX;
                this.startY = event.clientY;
            },
            mouseUp() {
                this.dragging = false;
                this.draggedNodeIndex = null;
            },
        }
    });
</script>


