<template>
    <svg :width="width" :height="height" style="border: 1px solid black;"
         @mousemove="mouseMove" @mouseup="mouseUp" @mouseleave="mouseUp">
        <circle v-for="(listnode, index) in listNodes" :key="'listnode'+index"
                :cx="listnode.x" :cy="listnode.y" :r="radius"
                style="fill: lightblue; stroke: black; stroke-width: 1px;"
                @mousedown="mouseDown(index, $event)" />
        <text v-for="(listnode, index) in listNodes" :key="'label'+index"
              :x="listnode.x" :y="listnode.y - 40" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 16px; user-select: none;">
            {{ listnode.label }}
        </text>
        <text v-for="(listnode, index) in listNodes" :key="'label'+index"
              :x="listnode.x" :y="listnode.y" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ listnode.value }}
        </text>
        <path v-for="(edge, index) in edges" :key="'edge' + index"
              :d="calculatePathD(edge)"
              :style="getPathStyle(edge)" />
        <marker id="arrowhead" markerWidth="10" markerHeight="10" refX="9" refY="3"
                orient="auto" markerUnits="strokeWidth">
            <path d="M0,0 L0,6 L9,3 z" fill="#000" />
        </marker>
        <marker id="arrowhead-start" markerWidth="10" markerHeight="10" refX="1" refY="3"
                orient="auto" markerUnits="strokeWidth">
            <path d="M9,0 L9,6 L0,3 z" fill="#000" />
        </marker>
    </svg>
</template>

<script lang="ts">
    import { defineComponent, onMounted, PropType } from 'vue';

    interface ListNode {
        id: number;
        listid: number;
        nextid: number; // Optional next ID
        label: string;
        x: number;
        y: number;
        value: number;
    }

    interface Edge {
        from: number; // Assuming these are indexes in the vertices array
        to: number;
    }

    export default defineComponent({
        name: 'ListVisualizer',
        props: {
            listNodes: Array as PropType<ListNode[]>,
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
                default: 30
            },
            isDoublyLinked: {
                type: Boolean,
                default: false
            }
        },
        computed: {
            edges() {
                //console.log("ListNodes: ", this.listNodes);
                const computedEdges = this.listNodes?.filter(node => node.nextid !== undefined)
                    ?.map(node => {
                        const parentNode = this.listNodes.find(parent => parent.id === node.nextid);
                        //console.log(`Looking for next node ${node.id} with parentId ${node.nextid}, found:`, parentNode);
                        if (!parentNode) {
                            return null;
                        }
                        return {
                            from: { x: parentNode.x, y: parentNode.y },
                            to: { x: node.x, y: node.y }
                        };
                    })
                    ?.filter(edge => edge !== null) || [];

                //console.log('Computed list edges:', computedEdges);
                return computedEdges;
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
            getPathStyle(edge: Edge) {
                return {
                    stroke: 'black',
                    strokeWidth: '2px',
                    fill: 'none',
                    markerEnd: 'url(#arrowhead)',
                    markerStart: this.isDoublyLinked ? 'url(#arrowhead-start)' : ''
                };
            },
            calculatePathD(edge: Edge) {
                const fromnode = edge.from;
                const tonode = edge.to;

                if (!fromnode || !tonode) {
                    console.error('Undefined list edge detected', { fromnode, tonode });
                    return ''; // Return an empty path if vertices are undefined
                }

                const dx = tonode.x - fromnode.x;
                const dy = tonode.y - fromnode.y;
                const dist = Math.sqrt(dx * dx + dy * dy);

                // Adjust start and end points to be on the edge of the vertex circles
                const offsetX = (dx * this.radius) / dist;
                const offsetY = (dy * this.radius) / dist;

                let startX = fromnode.x + offsetX;
                let startY = fromnode.y + offsetY;
                let endX = tonode.x - offsetX;
                let endY = tonode.y - offsetY;

                // If not doubly linked, ensure the arrow points from the current node to the next
                if (!this.isDoublyLinked) {
                    // Swap start and end points to reverse the arrow direction
                    [startX, endX] = [endX, startX];
                    [startY, endY] = [endY, startY];
                }

                return `M ${startX} ${startY} L ${endX} ${endY}`;
            },
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

                this.listNodes[this.draggedNodeIndex].x += dx;
                this.listNodes[this.draggedNodeIndex].y += dy;

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


