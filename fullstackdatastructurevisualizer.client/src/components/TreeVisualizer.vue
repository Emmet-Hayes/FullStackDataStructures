<template>
    <svg :width="width" :height="height" style="border: 1px solid black;"
         @mousemove="mouseMove" @mouseup="mouseUp" @mouseleave="mouseUp">
        <circle v-for="(treenode, index) in treeNodes" :key="'treenode'+index"
                :cx="treenode.x" :cy="treenode.y" :r="radius"
                style="fill: limegreen; stroke: black; stroke-width: 1px;"
                @mousedown="mouseDown(index, $event)" />
        <path v-for="(edge, index) in edges" :key="'edge' + index"
              :d="calculatePathD(edge)"
              style="stroke: black; stroke-width: 2px; fill: none;" />
        <text v-for="(treenode, index) in treeNodes" :key="'label'+index"
              :x="treenode.x" :y="treenode.y - 30" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 16px; user-select: none;">
            {{ treenode.label }}
        </text>
        <text v-for="(treenode, index) in treeNodes" :key="'label'+index"
              :x="treenode.x" :y="treenode.y" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ treenode.value }}
        </text>
    </svg>
    <button @click="isValidBST()">Validate BST</button>
    <span v-if="isValidBSTResult !== null">{{ isValidBSTResult ? 'True' : 'False' }}</span>
</template>

<script lang="ts">
    import { defineComponent, PropType } from 'vue';

    interface TreeNode {
        id: number;
        parentid?: number;
        label: string;
        x: number;
        y: number;
    }

    interface Edge {
        from: number; // Index of the parent node in the treeNodes array
        to: number; // Index of the child node in the treeNodes array
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
        data() {
            return {
                dragging: false,
                draggedNodeIndex: null,
                startX: 0,
                startY: 0,
                isValidBSTResult: null,
            };
        },
        computed: {
            edges() {
                const computedEdges = this.treeNodes?.filter(node => node.parentid !== undefined)
                    ?.map(node => {
                        const parentIndex = this.treeNodes.findIndex(parent => parent.id === node.parentid);
                        if (parentIndex === -1) {
                            return null;
                        }
                        return {
                            from: parentIndex,
                            to: this.treeNodes.indexOf(node)
                        };
                    })
                    ?.filter(edge => edge !== null) || [];

                return computedEdges;
            }
        },
        methods: {
            calculatePathD(edge: Edge) {
                const fromnode = this.treeNodes[edge.from];
                const tonode = this.treeNodes[edge.to];

                if (!fromnode || !tonode) {
                    console.error('Undefined tree node detected', { fromnode, tonode });
                    return ''; // Return an empty path if nodes are undefined
                }

                const dx = tonode.x - fromnode.x;
                const dy = tonode.y - fromnode.y;
                const dist = Math.sqrt(dx * dx + dy * dy);

                // Adjust start and end points to be on the edge of the circles
                const offsetX = (dx * this.radius) / dist;
                const offsetY = (dy * this.radius) / dist;

                const startX = fromnode.x + offsetX;
                const startY = fromnode.y + offsetY;
                const endX = tonode.x - offsetX;
                const endY = tonode.y - offsetY;

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

                this.treeNodes[this.draggedNodeIndex].x += dx;
                this.treeNodes[this.draggedNodeIndex].y += dy;

                // Update start position for next move event
                this.startX = event.clientX;
                this.startY = event.clientY;
            },
            mouseUp() {
                this.dragging = false;
                this.draggedNodeIndex = null;
            },
            isValidBST() {
                // Helper function to construct the tree and validate the BST property
                const validateNode = (nodeIndex, lowerBound, upperBound) => {
                    if (nodeIndex === -1) {
                        return true;
                    }

                    const nodeValue = parseInt(this.treeNodes[nodeIndex].label); // Assuming the label is the node's value

                    if ((lowerBound !== null && nodeValue <= lowerBound) || (upperBound !== null && nodeValue >= upperBound)) {
                        return false;
                    }

                    const leftChildIndex = this.treeNodes.findIndex(child => child.parentid === this.treeNodes[nodeIndex].id && child.x < this.treeNodes[nodeIndex].x);
                    const rightChildIndex = this.treeNodes.findIndex(child => child.parentid === this.treeNodes[nodeIndex].id && child.x > this.treeNodes[nodeIndex].x);

                    return validateNode(leftChildIndex, lowerBound, nodeValue) && validateNode(rightChildIndex, nodeValue, upperBound);
                };

                // Find the index of the root node (assuming it's the one with no parent)
                const rootNodeIndex = this.treeNodes.findIndex(node => node.parentid === undefined || node.parentid === null);

                if (rootNodeIndex === -1) {
                    console.error("No root node found");
                    return false;
                }

                this.isValidBSTResult = validateNode(rootNodeIndex, null, null);
            },
        }
    });
</script>

