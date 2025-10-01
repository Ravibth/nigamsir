export const GetRemainingLocations = (
  allLocations: any,
  selectedLocations: any
) => {
  const currentLocations = selectedLocations
    ?.filter((d: any) => d.isActive)
    .map((data: any) => {
      return data?.label.toString().toUpperCase();
    });
  return allLocations?.filter(
    (data: any) =>
      !currentLocations.includes(data?.label.toString().toUpperCase())
  );
};

export const capitalizeFirstLetter = (str: string) => {
  return str.charAt(0).toUpperCase() + str.slice(1);
};

export const GetRemainingLocations2 = (
  allLocations: any,
  selectedLocations: any
) => {
  const currentLocations = selectedLocations?.map((data: any) => {
    return data?.label?.toString().toUpperCase();
  });
  return allLocations?.filter(
    (data: any) =>
      !currentLocations.includes(data?.label.toString().toUpperCase())
  );
};
