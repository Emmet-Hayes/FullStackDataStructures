<template>
    <svg :width="width" :height="height" style="border: 1px solid black;"
         @mousemove="mouseMove" @mouseup="mouseUp" @mouseleave="mouseUp">
        <g v-for="(dictnode, index) in dictNodes" :key="'dictnode'+index"
           @mousedown="mouseDown(index, $event)">
            <rect :x="dictnode.x" :y="dictnode.y - size/2" :width="size" :height="size/2"
                  style="fill: lightskyblue; stroke: black; stroke-width: 1px;" />
            <rect :x="dictnode.x" :y="dictnode.y" :width="size" :height="size/2"
                  style="fill: lightseagreen; stroke: black; stroke-width: 1px;" />
            <text :x="dictnode.x + size/2" :y="dictnode.y - 50" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 16px; user-select: none;">
                {{ dictnode.label }}
            </text>
            <text :x="dictnode.x + size/2" :y="dictnode.y - 20" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 12px; user-select: none;">
                {{ dictnode.key }}
            </text>
            <text :x="dictnode.x + size/2" :y="dictnode.y + 20" alignment-baseline="middle" text-anchor="middle"
                  style="font-size: 12px; user-select: none;">
                {{ dictnode.value }}
            </text>
        </g>
    </svg>
</template>


<script lang="ts">
    import { defineComponent, onMounted, PropType } from 'vue';

    interface DictNode
    {
        id: number;
        listid: number;
        label: string;
        x: number;
        y: number;
    }

    interface Edge {
        from: number; // Assuming these are indexes in the dictNodes array
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

                this.dictNodes[this.draggedNodeIndex].x += dx;
                this.dictNodes[this.draggedNodeIndex].y += dy;

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


