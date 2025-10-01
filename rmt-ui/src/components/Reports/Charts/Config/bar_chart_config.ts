export const bar_chart_config = {
  title: "Scheduled VS Actual Variance",
  subtitle: "MoM Comparison",
  height: 800,
  chartArea: { width: "70%", height: 500 },
  bar: { groupWidth: "75%", gap: 5 },
  annotations: {
    alwaysOutside: true,
    textStyle: { fontSize: 26, fontName: "Source Sans Pro", textAlign: "end" },
  },
  legend: { position: "bottom", alignment: "center" },
  colors: ["#87CEEB", "#808080", "#3385ff"],
  hAxis: {
    title: "Actual",
    minValue: 0,
  },
  vAxis: {
    title: "Allocated",
  },
};
