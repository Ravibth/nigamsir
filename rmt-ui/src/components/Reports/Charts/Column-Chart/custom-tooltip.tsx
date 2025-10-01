import { TOGGLE_CONSTANTS } from "../../constant";
import "./custom-tooltip.scss";

//Capacity Utilization tooltip
export const CustomTooltip = ({ active, payload, label, toggleValue }: any) => {
  if (active && payload && payload.length) {
    if (toggleValue === TOGGLE_CONSTANTS.TIME) {
      payload = payload.filter((item) => item.dataKey == "allocation");
    } else if (toggleValue === TOGGLE_CONSTANTS.COST) {
      payload = payload.filter((item) => item.dataKey == "allocated_cost");
    }
    const hoverdata = payload[0].payload;
    return (
      <div className="custom-tooltip">
        <p className="label">{`${label}`}</p>
        {/* <p className="label">Allocation Date: : {`${label}`}</p> */}
        <div>
          {payload.map((pld, index: number) => (
            //console.log(pld)
            <div className="tooltip-maindiv">
              <div>
                <span>
                  Total{" "}
                  {pld.dataKey.replaceAll("_", " ") +
                    (toggleValue === TOGGLE_CONSTANTS.TIME ? " hrs" : "")}{" "}
                  :{" "}
                </span>
                <span> {pld.value} </span>
              </div>

              {payload.length == index + 1 ? (
                <div>
                  <div style={{ color: "gray", paddingTop: 15 }}>
                    {toggleValue === TOGGLE_CONSTANTS.TIME ? (
                      <>
                        <span> Allocation % : </span>
                        <span>
                          {" "}
                          {pld.payload.capacity > 0
                            ? (
                                (pld.payload.allocation /
                                  pld.payload.capacity) *
                                100
                              ).toFixed(2)
                            : ""}{" "}
                          %
                          {/* {pld.payload.allocation_percent.toFixed(2)}{" "} */}
                        </span>
                      </>
                    ) : (
                      <>
                        <span> Allocation % : </span>
                        <span>
                          {" "}
                          {pld.payload.capacity > 0
                            ? (
                                (pld.payload.allocation /
                                  pld.payload.capacity) *
                                100
                              ).toFixed(2)
                            : ""}{" "}
                          %
                          {/* {pld.payload.allocation_percent.toFixed(2)}{" "} */}
                        </span>
                      </>
                    )}
                  </div>

                  <div style={{ color: "gray", paddingTop: 15 }}>
                    {toggleValue === TOGGLE_CONSTANTS.TIME ? (
                      <>
                        <span> Allocation Job- Chargeable Hours : </span>
                        <span> {pld.payload.allocated_chargable_hr} </span>
                      </>
                    ) : (
                      <>
                        <span> Allocation Job- Chargeable Cost : </span>
                        <span> {pld.payload.allocated_chargable_cost} </span>
                      </>
                    )}
                  </div>

                  <div style={{ color: "gray", paddingTop: 15 }}>
                    {toggleValue === TOGGLE_CONSTANTS.TIME ? (
                      <>
                        <span> Allocation Job- Non-Chargeable Hours : </span>
                        <span> {pld.payload.allocated_non_chargable_hr} </span>
                      </>
                    ) : (
                      <>
                        <span> Allocation Job- Non-Chargeable Cost : </span>
                        <span>
                          {" "}
                          {pld.payload.allocated_non_chargable_cost}{" "}
                        </span>
                      </>
                    )}
                  </div>

                  <div style={{ color: "gray", paddingTop: 15 }}>
                    {toggleValue === TOGGLE_CONSTANTS.TIME ? (
                      <>
                        <span> Actual Job- Chargeable Hours : </span>
                        <span> {pld.payload.job_chargeable_hours} </span>
                      </>
                    ) : (
                      <>
                        <span> Actual Job- Chargeable Cost : </span>
                        <span> {pld.payload.job_chargeable_cost} </span>
                      </>
                    )}
                  </div>

                  <div style={{ color: "gray", paddingTop: 15 }}>
                    {toggleValue === TOGGLE_CONSTANTS.TIME ? (
                      <>
                        <span> Actual Job- Non Chargeable Hours : </span>
                        <span> {pld.payload.job_non_chargeable_hours} </span>
                      </>
                    ) : (
                      <>
                        <span> Actual Job- Non Chargeable Cost : </span>
                        <span> {pld.payload.job_non_chargeable_cost} </span>
                      </>
                    )}
                  </div>
                  {/* <div style={{ color: "gray", paddingTop: 15 }}>
                    <span>Capacity : </span>
                    <span> {pld.payload.capacity_percentage} % </span>
                  </div> */}
                  {/* <div style={{ color: "gray", paddingTop: 15 }}>
                    <span>Pipeline : </span>
                    <span> {pld.payload.pipeline} </span>
                  </div> */}
                  <div style={{ paddingTop: 15 }}>
                    <span>Allocation Chargeability : </span>
                    <span>
                      {" "}
                      {pld.payload.capacity > 0
                        ? (
                            (pld.payload.allocated_chargable_hr /
                              pld.payload.capacity) *
                            100
                          ).toFixed(2)
                        : ""}{" "}
                      %
                      {/* {pld.payload.allocation_chargability_percent.toFixed(
                        2
                      )} %{" "} */}
                    </span>
                  </div>
                  <div style={{ paddingTop: 15 }}>
                    <span>Actual Chargeability : </span>
                    <span>
                      {" "}
                      {pld.payload.capacity > 0
                        ? (
                            (pld.payload.actual_log_hours /
                              pld.payload.capacity) *
                            100
                          ).toFixed(2)
                        : ""}{" "}
                      %
                      {/* {pld.payload.actual_chargability_percent.toFixed(
                        2
                      )} %{" "} */}
                    </span>
                  </div>
                </div>
              ) : (
                ""
              )}
            </div>
          ))}
        </div>
      </div>
    );
  }

  return null;
};
