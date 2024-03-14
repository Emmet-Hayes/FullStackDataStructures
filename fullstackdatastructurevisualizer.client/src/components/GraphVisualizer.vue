<template>
    <svg :width="width" :height="height" style="border: 1px solid black;"
         @mousemove="mouseMove" @mouseup="mouseUp" @mouseleave="mouseUp">
        <circle v-for="(vertex, index) in vertices" :key="'vertex'+index"
                :cx="vertex.x" :cy="vertex.y" :r="radius"
                :style="{ fill: vertex.highlighted ? 'tomato' : vertex.traversed ? 'limegreen' : vertex.unreachable ? 'violet' : 'lightsteelblue', stroke: 'black', 'stroke-width': '1px' }"
                @mousedown="mouseDown(index, $event)" />
        <text v-for="(vertex, index) in vertices" :key="'label'+index"
              :x="vertex.x" :y="vertex.y - 30" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 16px; user-select: none;">
            {{ vertex.label }}
        </text>
        <text v-for="(vertex, index) in vertices" :key="'label'+index"
              :x="vertex.x" :y="vertex.y" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none;">
            {{ vertex.value }}
        </text>
        <text v-for="(vertex, index) in vertices" :key="'label'+index"
              :x="vertex.x" :y="vertex.y + 30" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none; fill: blue">
            {{ vertex.depth }}
        </text>
        <text v-for="(vertex, index) in vertices" :key="'label'+index"
              :x="vertex.x" :y="vertex.y + 45" alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none; fill: green">
            {{ vertex.visitOrder }}
        </text>
        <path v-for="(edge, index) in edges" :key="'edge' + index"
              :d="calculatePathD(edge)"
              style="stroke: black; stroke-width: 2px; fill: none;"
              :marker-end="isDirected ? 'url(#arrowhead)' : ''" />
        <marker id="arrowhead" markerWidth="10" markerHeight="10" refX="9" refY="3"
                orient="auto" markerUnits="strokeWidth">
            <path d="M0,0 L0,6 L9,3 z" fill="#000" />
        </marker>
        <text v-for="(edge, index) in edges" v-if="edge?.weight !== 1" :key="'weight' + index"
              :x="calculateMidpoint(edge).x" :y="calculateMidpoint(edge).y"
              alignment-baseline="middle" text-anchor="middle"
              style="font-size: 12px; user-select: none; fill: red;">
            {{ edge.weight }}
        </text>
    </svg>
    <button @click="bfs(1)">BFS</button>
    <button @click="dfs(1)">DFS</button>
    <button @click="dijkstra(1)">Dijkstra</button>
    <button @click="clearTraversal()">clear</button>
</template>

<script lang="ts">
    import { defineComponent, onMounted, watch } from 'vue';

    interface vertex
    {
        x: number;
        y: number;
        label: string;
        depth: number | null;
        visitOrder: number | null;
    }

    interface Edge
    {
        from: number;
        to: number;
        weight: number;
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
        data() {
            return {
                dragging: false,
                draggedVertexIndex: null,
                startX: 0,
                startY: 0,
            };
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
            },
            mouseDown(index, event) {
                this.dragging = true;
                this.draggedVertexIndex = index;
                this.startX = event.clientX;
                this.startY = event.clientY;
            },
            mouseMove(event) {
                if (!this.dragging || this.draggedVertexIndex === null) return;

                const dx = event.clientX - this.startX;
                const dy = event.clientY - this.startY;

                this.vertices[this.draggedVertexIndex].x += dx;
                this.vertices[this.draggedVertexIndex].y += dy;

                // Update start position for next move event
                this.startX = event.clientX;
                this.startY = event.clientY;
            },
            mouseUp() {
                this.dragging = false;
                this.draggedVertexIndex = null;
            },
            async bfs(startIndex: number) {
                const visited = new Array(this.vertices.length).fill(false);
                const queue: Array<{ index: number, depth: number }> = [{ index: startIndex, depth: 0 }];
                let visitCounter = 0;
                visited[startIndex] = true;

                // Initialize all vertices' extra fields to null
                this.vertices.forEach(vertex => {
                    vertex.depth = null;
                    vertex.visitOrder = null;
                    vertex.traversed = false;
                    vertex.unreachable = false;
                    vertex.highlighted = false;
                });

                // Function to introduce delay
                const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

                while (queue.length > 0) {
                    const { index: vertexIndex, depth } = queue.shift();
                    console.log(`Visited vertex: ${this.vertices[vertexIndex].label} at depth ${depth}`);

                    // Visualize the current vertex and update its depth
                    this.vertices[vertexIndex].highlighted = true;
                    this.vertices[vertexIndex].depth = depth;
                    this.vertices[vertexIndex].visitOrder = ++visitCounter;

                    // Wait for a bit before moving on to the next vertex
                    await delay(500); // Adjust delay time as needed

                    // Add unvisited adjacent vertices to the queue
                    this.edges.forEach((edge) => {
                        if (edge.from === vertexIndex && !visited[edge.to]) {
                            queue.push({ index: edge.to, depth: depth + 1 });
                            visited[edge.to] = true;
                        } else if (!this.isDirected && edge.to === vertexIndex && !visited[edge.from]) {
                            queue.push({ index: edge.from, depth: depth + 1 });
                            visited[edge.from] = true;
                        }
                    });

                    this.vertices[vertexIndex].highlighted = false;
                    this.vertices[vertexIndex].traversed = true;
                }

                // Continue from any unvisited nodes
                for (let i = 0; i < this.vertices.length; i++) {
                    if (!visited[i]) {
                        // Mark as 'Infinity' since it was not visited in the main traversal
                        this.vertices[i].depth = Infinity;
                        this.vertices[i].visitOrder = ++visitCounter;
                        this.vertices[i].highlighted = true;
                        await delay(500); // Wait for visualization
                        this.vertices[i].highlighted = false;
                        this.vertices[i].unreachable = true;
                    }
                }
            },
            async dfs(startIndex: number) {
                const visited = new Array(this.vertices.length).fill(false);
                let visitCounter = 0; // global counter for depth
                const stack: Array<{ index: number, depth: number }> = [{ index: startIndex, depth: 0 }];
                visited[startIndex] = true;

                // Initialize all vertices' depths to null and reset traversed state
                this.vertices.forEach(vertex => {
                    vertex.depth = null;
                    vertex.visitOrder = null;
                    vertex.traversed = false;
                    vertex.unreachable = false;
                    vertex.highlighted = false; // Ensure all nodes are unhighlighted initially
                });

                // Function to introduce delay
                const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

                while (stack.length > 0) {
                    const { index: vertexIndex, depth } = stack.pop();
                    console.log(`Visited vertex: ${this.vertices[vertexIndex].label} at depth ${depth}`);

                    // Skip if this vertex was processed after being added to the stack in another iteration
                    if (this.vertices[vertexIndex].traversed) continue;

                    // Visualize the current vertex, update its depth, and mark it as traversed
                    this.vertices[vertexIndex].highlighted = true;
                    this.vertices[vertexIndex].depth = depth;
                    this.vertices[vertexIndex].visitOrder = ++visitCounter;
                    this.vertices[vertexIndex].traversed = true;

                    await delay(500); // Wait for visualization

                    // Find and add unvisited adjacent vertices to the stack
                    this.edges.forEach(edge => {
                        if (edge.from === vertexIndex && !visited[edge.to]) {
                            stack.push({ index: edge.to, depth: depth + 1 });
                            visited[edge.to] = true;
                        } else if (!this.isDirected && edge.to === vertexIndex && !visited[edge.from]) {
                            stack.push({ index: edge.from, depth: depth + 1 });
                            visited[edge.from] = true;
                        }
                    });

                    // Unhighlight the node after pushing all adjacent nodes to stack
                    // but before the next iteration to simulate traversal
                    this.vertices[vertexIndex].highlighted = false;
                    this.vertices[vertexIndex].traversed = true;
                }

                // Continue from any unvisited nodes
                for (let i = 0; i < this.vertices.length; i++) {
                    if (!visited[i]) {
                        // Mark as 'Infinity' since it was not visited in the main traversal
                        this.vertices[i].depth = Infinity;
                        this.vertices[i].visitOrder = ++visitCounter;
                        this.vertices[i].highlighted = true;
                        await delay(500); // Wait for visualization
                        this.vertices[i].highlighted = false;
                        this.vertices[i].unreachable = true;
                    }
                }
            },
            async dijkstra(startIndex: number) {
                const distances: Array<number> = new Array(this.vertices.length).fill(Infinity);
                const visited: Array<boolean> = new Array(this.vertices.length).fill(false);
                const previous: Array<number | null> = new Array(this.vertices.length).fill(null);
                let visitCounter = 0;

                // Duplicate reversed edges if the graph is undirected
                let traversalEdges = this.edges.slice(); // Copy the original edges
                if (!this.isDirected) {
                    this.edges.forEach(edge => {
                        traversalEdges.push({ from: edge.to, to: edge.from, weight: edge.weight });
                    });
                }

                // Initialize all vertices' depths to null and reset traversed state
                this.vertices.forEach(vertex => {
                    vertex.depth = null;
                    vertex.visitOrder = null;
                    vertex.traversed = false;
                    vertex.highlighted = false; // Ensure all nodes are unhighlighted initially
                });

                distances[startIndex] = 0;

                const priorityQueue: Array<{ index: number, distance: number }> = this.vertices.map((_, index) => ({
                    index,
                    distance: distances[index],
                }));

                // Function to introduce delay
                const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));

                while (priorityQueue.length > 0) {
                    priorityQueue.sort((a, b) => a.distance - b.distance);
                    const { index: u } = priorityQueue.shift();

                    if (visited[u]) continue;
                    visited[u] = true;

                    // Visualize the current vertex
                    this.vertices[u].visitOrder = ++visitCounter;
                    this.vertices[u].depth = distances[u];
                    this.vertices[u].highlighted = true;

                    await delay(500); // Adjust delay time as needed

                    traversalEdges.forEach(edge => {
                        if (edge.from === u && !visited[edge.to]) {
                            const alt = distances[u] + edge.weight;
                            if (alt < distances[edge.to]) {
                                distances[edge.to] = alt;
                                previous[edge.to] = u;
                                priorityQueue.push({ index: edge.to, distance: alt });
                            }
                        }
                    });

                    // Unhighlight and mark as traversed or unreachable for visualization purposes
                    this.vertices[u].highlighted = false;
                    if (distances[u] === Infinity)
                        this.vertices[u].unreachable = true;
                    else
                        this.vertices[u].traversed = true;
                }
            },
            async clearTraversal()
            {
                // Initialize all vertices' depths to null and reset traversed state
                this.vertices.forEach(vertex => {
                    vertex.depth = null;
                    vertex.visitOrder = null;
                    vertex.traversed = false;
                    vertex.unreachable = false;
                    vertex.highlighted = false; // Ensure all nodes are unhighlighted initially
                });
            }
        }
    });
</script>

