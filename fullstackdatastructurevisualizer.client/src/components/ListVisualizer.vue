<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <circle v-for="(listnode, index) in listNodes" :key="'listnode'+index"
                :cx="listnode.x" :cy="listnode.y" :r="radius"
                style="fill: lightblue; stroke: black; stroke-width: 1px;" />
        <path v-for="(edge, index) in edges" :key="'edge' + index"
                :d="calculatePathD(edge)"
                style="stroke: black; stroke-width: 2px; fill: none; marker-end: url(#arrowhead);" />
        <text v-for="(listnode, index) in listNodes" :key="'label'+index"
                :x="listnode.x" :y="listnode.y - 10" alignment-baseline="middle" text-anchor="middle"
                style="font-size: 12px; user-select: none;">
            {{ listnode.label }}
        </text>
        <marker id="arrowhead" markerWidth="10" markerHeight="10" refX="9" refY="3"
                orient="auto" markerUnits="strokeWidth">
            <path d="M0,0 L0,6 L9,3 z" fill="#000" />
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
        },
        computed: {
            edges() {
                console.log("ListNodes: ", this.listNodes);
                const computedEdges = this.listNodes?.filter(node => node.nextid !== undefined)
                    ?.map(node => {
                        const parentNode = this.listNodes.find(parent => parent.id === node.nextid);
                        console.log(`Looking for next node ${node.id} with parentId ${node.nextid}, found:`, parentNode);
                        if (!parentNode) {
                            return null;
                        }
                        return {
                            from: { x: parentNode.x, y: parentNode.y },
                            to: { x: node.x, y: node.y }
                        };
                    })
                    ?.filter(edge => edge !== null) || [];

                console.log('Computed list edges:', computedEdges);
                return computedEdges;
            }
        },
        methods: {
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

                const startX = fromnode.x + offsetX;
                const startY = fromnode.y + offsetY;
                const endX = tonode.x - offsetX;
                const endY = tonode.y - offsetY;

                return `M ${startX} ${startY} L ${endX} ${endY}`;
            },
        }
    });

</script>


