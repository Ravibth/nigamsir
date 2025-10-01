export const GetChargablityPercentage = (
  total: number,
  job_chargable: number
) => {
  if (total > 0) {
   // console.log(total, job_chargable);
    return Number(((job_chargable / total) * 100).toFixed(2));
  }
  return 0;
};

export const GetCapacityPercentage = (
  total: number,
  job_chargable: number,
  job_non_chargeable: number
) => {
  if (total > 0) {
   //console.log(total, job_chargable, job_non_chargeable);
    return Number(
      (((job_chargable + job_non_chargeable) / total) * 100).toFixed(2)
    );
  }
  return 0;
};



