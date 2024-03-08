<template>
  <svg :width="width" :height="height" style="border: 1px solid black;">
    <circle v-for="(node, index) in nodes" :key="'node'+index"
            :cx="node.x" :cy="node.y" :r="radius"
            style="fill: lightblue; stroke: black; stroke-width: 1px;"
    />
    <path v-for="(edge, index) in edges" :key="'edge'+index"
          :d="'M'+nodes[edge.from].x+' '+nodes[edge.from].y+' L '+nodes[edge.to].x+' '+nodes[edge.to].y"
          style="stroke: black; stroke-width: 2px; fill: none;"
    />
    <text v-for="(node, index) in nodes" :key="'label'+index"
          :x="node.x" :y="node.y" alignment-baseline="middle" text-anchor="middle"
          style="font-size: 12px; user-select: none;"
    >
      {{ node.label }}
    </text>
  </svg>
</template>

<script>
export default {
  name: 'GraphVisualizer',
  props: {
    nodes: Array,
    edges: Array,
    width: {
      type: Number,
      default: 600
    },
    height: {
      type: Number,
      default: 400
    },
    radius: {
      type: Number,
      default: 20
    }
  }
}
</script>
