<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <circle v-for="(node, index) in nodes" :key="'node'+index"
                :cx="node.x" :cy="node.y" :r="radius"
                style="fill: lightpink; stroke: black; stroke-width: 1px;" />
        <marker id="arrowhead" markerWidth="10" markerHeight="10" refX="9" refY="3"
                orient="auto" markerUnits="strokeWidth">
            <path d="M0,0 L0,6 L9,3 z" fill="#000" />
        </marker>
        <path v-for="(edge, index) in edges" :key="'edge' + index"
              :d="calculatePathD(edge)"
              style="stroke: black; stroke-width: 2px; fill: none;"
              :marker-end="isDirected ? 'url(#arrowhead)' : ''" />
        <!--
        <text v-for="(edge, index) in edges" v-if="edge.weight !== 1" :key="'weight' + index"
              :x="calculateMidpoint(edge).x" :y="calculateMidpoint(edge).y"
              alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none; fill: red;">
            {{ edge.weight }}
        </text> 
        -->
        <text v-for="(node, index) in nodes" :key="'label'+index"
              :x="node.x" :y="node.y" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ node.label }}
        </text>
    </svg>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    interface Node {
        x: number;
        y: number;
        label: string;
    }

    interface Edge {
        from: number; // Assuming these are indexes in the nodes array
        to: number;
    }

    export default defineComponent({
        name: 'GraphVisualizer',
        props: {
            nodes: Array as () => Node[],
            edges: Array as () => Edge[],
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
            isDirected: {
                type: Boolean,
                default: false
            }
        },
        methods: {
            calculatePathD(edge: Edge) {
                console.log("Nodes:", this.nodes);
                console.log("edge:", edge);
                const fromNode = this.nodes[edge.from];
                const toNode = this.nodes[edge.to];
                console.log('From Node:', fromNode, 'To Node:', toNode);

                if (!fromNode || !toNode) {
                    console.error('Undefined node detected', { fromNode, toNode });
                    return ''; // Return an empty path if nodes are undefined
                }
                const dx = toNode.x - fromNode.x;
                const dy = toNode.y - fromNode.y;
                const dist = Math.sqrt(dx * dx + dy * dy);

                // Adjust start and end points to be on the edge of the node circles
                const offsetX = (dx * this.radius) / dist;
                const offsetY = (dy * this.radius) / dist;

                const startX = fromNode.x + offsetX;
                const startY = fromNode.y + offsetY;
                const endX = toNode.x - offsetX;
                const endY = toNode.y - offsetY;

                return `M ${startX} ${startY} L ${endX} ${endY}`;
            },
            calculateMidpoint(edge: Edge) {
                const fromNode = this.nodes[edge.from];
                const toNode = this.nodes[edge.to];
                return {
                    x: (fromNode.x + toNode.x) / 2,
                    y: (fromNode.y + fromNode.y) / 2
                };
            }
        }
    });
</script>

