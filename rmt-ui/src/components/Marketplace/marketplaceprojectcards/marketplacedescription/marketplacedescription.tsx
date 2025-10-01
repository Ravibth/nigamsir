import React from "react";

const Marketplacedescription = (props: any) => {
  const projectData = {
    description:
      "This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and values strong internal controls.This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and values strong internal controls.",
  };
  return (
    <div
      style={{
        paddingTop: "30px",
        fontSize: "17px",
      }}
    >
      <div style={{ fontWeight: "bold" }} className="description-header">
        Description
      </div>
      <div className="description-data">{props.projectdescription}</div>
    </div>
  );
};

export default Marketplacedescription;
