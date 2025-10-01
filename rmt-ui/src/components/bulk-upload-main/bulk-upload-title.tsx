import React from "react";

const BulkUploadTitle = (props: any) => {
  const { projectDetails } = props;

  const JobCodePresent = projectDetails?.jobCode;
  return (
    <>
      {JobCodePresent ? (
        <div className="bulk-title">{projectDetails?.jobName}</div>
      ) : (
        <div className="bulk-title">{projectDetails?.pipelineName}</div>
      )}
    </>
  );
};

export default BulkUploadTitle;
