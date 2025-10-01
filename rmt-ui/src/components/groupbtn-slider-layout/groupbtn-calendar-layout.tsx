import React from "react";
import DaysGrpBtn from "../daysgroup/days-groups";
import DaysGrpCalBtn from "../daysgroup/days-groups-calendar";
import ViewSlider from "../view-slider-bar/viewslider";

const GroupbtnCalendarlayout = (props: any) => {
  const { handleSchedulerViewChange } = props;

  return (
    <React.Fragment>
      <DaysGrpCalBtn handleSchedulerViewChange={handleSchedulerViewChange} />
      {/* <ViewSlider /> */}
    </React.Fragment>
  );
};

export default GroupbtnCalendarlayout;
