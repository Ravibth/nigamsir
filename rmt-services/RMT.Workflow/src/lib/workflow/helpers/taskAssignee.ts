import { EWorkflowTaskAssignedType } from "../enum";

interface ITaskAssignee {
  key: EWorkflowTaskAssignedType;
  value: string;
}

export const transformTaskAssignee = (values: ITaskAssignee[]): string => {
  try {
    return values.reduce((acc, value, index) => {
      acc += `${index == 0 ? "" : "_"}${value.key}$${value.value}`;
      return acc;
    }, "");
  } catch (e) {
    throw e;
  }
};

export const getTaskAssignee = (str: string): ITaskAssignee[] => {
  try {
    const mapping = str.split("_");

    return mapping.reduce((acc, nextValue) => {
      const [key, value] = nextValue.split("$");

      acc.push({ key, value });
      return acc;
    }, []);
  } catch (e) {
    throw e;
  }
};
