<template>
    <svg :width="width" :height="height" style="border: 1px solid black;">
        <circle v-for="(treenode, index) in treeNodes" :key="'treenode'+index"
                :cx="treenode.x" :cy="treenode.y" :r="radius"
                style="fill: lightpink; stroke: black; stroke-width: 1px;" />
        <path v-for="(edge, index) in edges" :key="'edge' + index"
              :d="calculatePathD(edge)"
              style="stroke: black; stroke-width: 2px; fill: none;" />
        <text v-for="(treenode, index) in treeNodes" :key="'label'+index"
              :x="treenode.x" :y="treenode.y" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ treenode.label }}
        </text>
    </svg>
</template>

<script lang="ts">
    import { defineComponent, onMounted, PropType } from 'vue';

    interface TreeNode {
        id: number;
        parentId?: number; // Optional parent ID
        label: string;
        x: number;
        y: number;
    }

    interface Edge {
        from: number; // Assuming these are indexes in the vertices array
        to: number;
    }

    export default defineComponent({
        name: 'TreeVisualizer',
        props: {
            treeNodes: Array as PropType<TreeNode[]>,
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
                console.log("TreeNodes: ", this.treeNodes)
                const edgeList = this.treeNodes
                    .filter(node => node.parentId !== undefined) // Only consider nodes with a parent
                    .map(node => {
                        // Find the parent node index
                        const parentNodeIndex = this.treeNodes.findIndex(parent => parent.id === node.parentId);
                        if (parentNodeIndex === -1) {
                            console.error('Parent node not found for node', node);
                            return null;
                        }
                        // Find the current node index
                        const currentNodeIndex = this.treeNodes.indexOf(node);
                        return { from: parentNodeIndex, to: currentNodeIndex };
                    })
                    .filter(edge => edge !== null); // Remove any null entries
                console.log('Generated edges:', edgeList);
                return edgeList;
            }
        },
        methods: {
            calculatePathD(edge: Edge) {
                const fromnode = this.treeNodes[edge.from];
                const tonode = this.treeNodes[edge.to];

                if (!fromnode || !tonode) {
                    console.error('Undefined vertex detected', { fromnode, tonode });
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


