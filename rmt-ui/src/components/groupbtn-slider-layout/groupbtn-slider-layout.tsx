import React from "react";
import DaysGrpBtn from "../daysgroup/days-groups";
import DaysGrpCalBtn from "../daysgroup/days-groups-calendar";
import ViewSlider from "../view-slider-bar/viewslider";

const Groupbtnsliderlayout = (props: any) => {
  const { handleSchedulerViewChange } = props;

  return (
    <React.Fragment>
      <DaysGrpBtn handleSchedulerViewChange={handleSchedulerViewChange} />
      {/* <ViewSlider /> */}
    </React.Fragment>
  );
};

export default Groupbtnsliderlayout;
