import { TOGGLE_CONSTANTS } from "../../../constant";
import "./custom_tooltip.scss";

//Schedule Variance Chart Tooltip
export const CustomTooltip = ({ active, payload, label, toggleValue }: any) => {
  if (active && payload && payload.length) {
    const hoverData = payload[0].payload;
    return (
      <div className="custom-tooltip">
        <p className="label">{`${label}`}</p>
        <div style={{ color: "gray", paddingTop: 15 }}>
          <span>Capacity : </span>
          <span>
            {toggleValue?.toLowerCase()?.trim() ===
            TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
              ? hoverData?.capacity + " hrs"
              : hoverData?.capacity_cost}
          </span>
        </div>
        <div style={{ color: "gray", paddingTop: 15 }}>
          <span>Allocation : </span>
          <span>
            {toggleValue?.toLowerCase()?.trim() ===
            TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
              ? hoverData.allocation_hours + " hrs"
              : hoverData.allocated_cost}
          </span>
          <span> | </span>
          <span>Actual : </span>
          <span>
            {toggleValue?.toLowerCase()?.trim() ===
            TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
              ? hoverData.actual_log_hours + " hrs"
              : hoverData.actual_cost}
          </span>
        </div>
        <div style={{ color: "gray", paddingTop: 15 }}>
          <span>Allocation% : </span>
          <span>
            {hoverData.capacity > 0
              ? (
                  (hoverData.allocation_hours / hoverData.capacity) *
                  100
                ).toFixed(2)
              : ""}{" "}
            %
          </span>
          <span> | </span>
          <span>Actual% : </span>
          <span>
            {hoverData.capacity > 0
              ? (
                  (hoverData.actual_log_hours / hoverData.capacity) *
                  100
                ).toFixed(2)
              : ""}{" "}
            %
          </span>
        </div>
        <div style={{ color: "gray", paddingTop: 15 }}>
          <span>Job Chargeable - Allocation: </span>
          <span>
            {toggleValue?.toLowerCase()?.trim() ===
            TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
              ? hoverData.allocated_chargable_hr + " hrs"
              : hoverData.allocated_chargable_cost}
          </span>
          <span> | </span>
          <span>Actual : </span>
          <span>
            {toggleValue?.toLowerCase()?.trim() ===
            TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
              ? hoverData.job_chargeable_hours + " hrs"
              : hoverData.job_chargeable_cost}
          </span>
        </div>
        <div style={{ color: "gray", paddingTop: 15 }}>
          <span>Job Non- Chargeable - Allocation: </span>
          <span>
            {toggleValue?.toLowerCase()?.trim() ===
            TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
              ? hoverData.allocated_non_chargable_hr + " hrs"
              : hoverData.allocated_non_chargable_cost}
          </span>
          <span> | </span>
          <span>Actual : </span>
          <span>
            {toggleValue?.toLowerCase()?.trim() ===
            TOGGLE_CONSTANTS.TIME.toLowerCase().trim()
              ? hoverData.job_non_chargeable_hours + " hrs"
              : hoverData.job_non_chargeable_cost}
          </span>
        </div>
        <div style={{ color: "gray", paddingTop: 15 }}>
          <span>Chargeability – Allocation: </span>
          <span>
            {hoverData.capacity > 0
              ? (
                  (hoverData.allocated_chargable_hr / hoverData.capacity) *
                  100
                ).toFixed(2)
              : ""}{" "}
          </span>
          <span> | </span>
          <span>Actual : </span>
          <span>
            {hoverData.capacity > 0
              ? (
                  (hoverData.job_chargeable_hours / hoverData.capacity) *
                  100
                ).toFixed(2)
              : ""}{" "}
          </span>
        </div>
      </div>
    );
  }

  return null;
};
