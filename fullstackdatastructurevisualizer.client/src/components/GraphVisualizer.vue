<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <circle v-for="(vertex, index) in vertices" :key="'vertex'+index"
                :cx="vertex.x" :cy="vertex.y" :r="radius"
                style="fill: lightsteelblue; stroke: black; stroke-width: 1px;" />
        <marker id="arrowhead" markerWidth="10" markerHeight="10" refX="9" refY="3"
                orient="auto" markerUnits="strokeWidth">
            <path d="M0,0 L0,6 L9,3 z" fill="#000" />
        </marker>
        <path v-for="(edge, index) in edges" :key="'edge' + index"
              :d="calculatePathD(edge)"
              style="stroke: black; stroke-width: 2px; fill: none;"
              :marker-end="isDirected ? 'url(#arrowhead)' : ''" />
        <text v-for="(edge, index) in edges" v-if="edge?.weight !== 1" :key="'weight' + index"
              :x="calculateMidpoint(edge).x" :y="calculateMidpoint(edge).y"
              alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none; fill: red;">
            {{ edge.weight }}
        </text>
        <text v-for="(vertex, index) in vertices" :key="'label'+index"
              :x="vertex.x" :y="vertex.y - 30" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ vertex.label }}
        </text>
        <text v-for="(vertex, index) in vertices" :key="'label'+index"
              :x="vertex.x" :y="vertex.y" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ vertex.value }}
        </text>
    </svg>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    interface vertex
    {
        x: number;
        y: number;
        label: string;
    }

    interface Edge
    {
        from: number; // Assuming these are indexes in the vertices array
        to: number;
    }

    export default defineComponent({
        name: 'GraphVisualizer',
        props: {
            vertices: Array as () => vertex[],
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
                const fromvertex = this.vertices[edge.from];
                const tovertex = this.vertices[edge.to];

                if (!fromvertex || !tovertex) {
                    console.error('Undefined vertex detected', { fromvertex, tovertex });
                    return ''; // Return an empty path if vertices are undefined
                }

                const dx = tovertex.x - fromvertex.x;
                const dy = tovertex.y - fromvertex.y;
                const dist = Math.sqrt(dx * dx + dy * dy);

                // Adjust start and end points to be on the edge of the vertex circles
                const offsetX = (dx * this.radius) / dist;
                const offsetY = (dy * this.radius) / dist;

                const startX = fromvertex.x + offsetX;
                const startY = fromvertex.y + offsetY;
                const endX = tovertex.x - offsetX;
                const endY = tovertex.y - offsetY;

                return `M ${startX} ${startY} L ${endX} ${endY}`;
            },
            calculateMidpoint(edge: Edge) {
                const fromvertex = this.vertices[edge.from];
                const tovertex = this.vertices[edge.to];

                if (!fromvertex || !tovertex) {
                    console.error('Undefined edge detected', { fromvertex, tovertex });
                    return ''; // Return an empty path if vertices are undefined
                }

                return {
                    x: (fromvertex.x + tovertex.x) / 2,
                    y: (fromvertex.y + tovertex.y - 30) / 2
                };
            }
        }
    });
</script>

