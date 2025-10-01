import dayjs from "dayjs";
import moment from "moment";
import { CHART_VIEW_TYPE } from "./scheduled_vs_variance/constant";
export const DATETIME_FORMAT = "MMM-YY";
export const formateDate = (date) => {
  return moment(date).format("YYYY-MM-DD");
};

export const formateDateAfterRendering = (data) => {
  return data.map(
    (item) =>
      (item.date = dayjs(new Date(item.date))
        .format(DATETIME_FORMAT)
        .toUpperCase())
  );
};

export const formateDateWithoutChangingData = (data) => {
  return data.map((item) => {
    return {
      newDate: dayjs(new Date(item.date)).format(DATETIME_FORMAT).toUpperCase(),
      year: item.date.split(" ")[0],
      month: item.date.split(" ")[1],
      ...item,
    };
  });
};

export const calculatePercentage = (a, b) => {
  if (b > 0) {
    return ((a * 100) / b).toFixed(2);
  }
  return 0;
};

export const sortdate = (date) => {
  return date.sort(function (x, y) {

    const date1: any = new Date(x.date);
    const date2: any = new Date(y.date);
    return date2 - date1;
  });
};

//Schedule variance chart
export const transformData = (data) => {
  const d1 = data;
  return d1.reduce((acc, curr) => {
    const objInAcc = acc.find(
      (o) =>
        new Date(o.date).getMonth() === new Date(curr.date).getMonth() &&
        new Date(o.date).getFullYear() === new Date(curr.date).getFullYear()
    );
    if (objInAcc) {
      objInAcc.capacity += curr.capacity;
      objInAcc.actual_log_hours += curr.actual_log_hours; //c
      objInAcc.allocation_hours += curr.allocation_hours;
      objInAcc.availability += curr.availability;
      objInAcc.availability_cost += curr.availability_cost;

      objInAcc.actual_percentage = calculatePercentage(
        objInAcc.actual_log_hours, //c
        objInAcc.capacity
      );
      objInAcc.allocation_percentage = calculatePercentage(
        objInAcc.allocation_hours,
        objInAcc.capacity
      );
      objInAcc.allocation_cost_percentage = calculatePercentage(
        objInAcc.allocated_cost,
        objInAcc.capacity_cost
      );
      objInAcc.actual_cost_percentage = calculatePercentage(
        objInAcc.actual_cost,
        objInAcc.capacity_cost
      );
      objInAcc.allocation_chargeability_percentage = calculatePercentage(
        objInAcc.allocation_hours,
        objInAcc.capacity
      );
      objInAcc.actual_chargeability_percentage = calculatePercentage(
        //todo: - doubt related tooltip
        objInAcc.actual_log_hours, //c
        objInAcc.capacity
      );
    } else {
      curr.cost_amount = 100;
      curr.actual_percentage = (
        (curr.actual_log_hours * 100) /
        curr.capacity
      ).toFixed(2); //c
      curr.allocation_percentage = calculatePercentage(
        curr.allocation_hours,
        curr.capacity
      );

      curr.allocation_cost_percentage = calculatePercentage(
        curr.allocated_cost,
        curr.capacity_cost
        // curr.capacity,
      );
      curr.actual_cost_percentage = calculatePercentage(
        curr.actual_cost,
        curr.capacity_cost
        // curr.capacity
      );
      curr.chargeability_percentage = calculatePercentage(
        curr.allocation_hours,
        curr.capacity
      );
      acc.push(curr);
    }
    return acc;
  }, []);
};

export const transformDataForGrid = (data) => {
  // console.log(data);
  return data.reduce((acc, curr) => {
    const objInAcc = acc.find(
      (o) =>
        new Date(o.date).getMonth() === new Date(curr.date).getMonth() &&
        new Date(o.date).getFullYear() === new Date(curr.date).getFullYear() &&
        o.email_id.toLowerCase().trim() === curr.email_id.toLowerCase().trim()
    );
    if (objInAcc) {
      objInAcc.capacity += curr.capacity;
      objInAcc.actual_log_hours += curr.actual_log_hours; //c
      objInAcc.allocation_hours += curr.allocation_hours;
      objInAcc.availability += curr.availability;
      objInAcc.availability_cost += curr.availability_cost;

      objInAcc.actual_percentage = calculatePercentage(
        objInAcc.actual_log_hours, //c
        objInAcc.capacity
      );
      objInAcc.allocation_percentage = calculatePercentage(
        objInAcc.allocation_hours,
        objInAcc.capacity
      );
      objInAcc.allocation_cost_percentage = calculatePercentage(
        objInAcc.allocated_cost,
        objInAcc.capacity
      );
      objInAcc.actual_cost_percentage = calculatePercentage(
        objInAcc.actual_cost,
        objInAcc.capacity
      );
      objInAcc.allocation_chargeability_percentage = calculatePercentage(
        objInAcc.allocation_hours,
        objInAcc.capacity
      );
      objInAcc.actual_chargeability_percentage = calculatePercentage(
        //todo: - doubt related tooltip
        objInAcc.actual_log_hours, //c
        objInAcc.capacity
      );
    } else {
      curr.cost_amount = 100;
      curr.actual_percentage = (
        (curr.actual_log_hours * 100) /
        curr.capacity
      ).toFixed(2); //c
      curr.allocation_percentage = calculatePercentage(
        curr.allocation_hours,
        curr.capacity
      );

      curr.allocation_cost_percentage = calculatePercentage(
        curr.allocated_cost,
        curr.capacity_cost
        // curr.capacity,
      );
      curr.actual_cost_percentage = calculatePercentage(
        curr.actual_cost,
        curr.capacity_cost
        // curr.capacity
      );
      curr.chargeability_percentage = calculatePercentage(
        curr.allocation_hours,
        curr.capacity
      );
      acc.push(curr);
    }
    return acc;
  }, []);
};

export const GetAgGridColumnDefForTime = () => {
  const columnDefs: any = [
    {
      headerName: "Date",
      field: "date",
      suppressMenu: true,
      tooltipField: "action",
    },
    {
      headerName: "Employee",
      headerTooltip: "Employee",
      field: "email_id",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Location",
      headerTooltip: "Location",
      field: "location",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Designation",
      headerTooltip: "Designation",
      field: "designation_name",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Business Unit",
      headerTooltip: "Business Unit",
      field: "business_unit",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    // {
    //   headerName: "Expertise",
    //   headerTooltip: "Expertise",
    //   field: "expertise",
    //   // flex: 0.8,
    //   filter: "agTextColumnFilter",
    //   filterParams: {
    //     suppressAndOrCondition: true,
    //   },
    //   sortable: true,
    //   unSortIcon: true,
    //   menuTabs: ["filterMenuTab"],
    //   tooltipField: "",
    // },
    // {
    //   headerName: "SMEG",
    //   headerTooltip: "SMEG",
    //   field: "sme_group_name",
    //   // flex: 0.8,
    //   filter: "agTextColumnFilter",
    //   filterParams: {
    //     suppressAndOrCondition: true,
    //   },
    //   sortable: true,
    //   unSortIcon: true,
    //   menuTabs: ["filterMenuTab"],
    //   tooltipField: "",
    // },
    // Actual Log Hours
    {
      headerName: "Actual Log Hours",
      field: "actual_log_hours",
      headerTooltip: "Actual Log Hours",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    // Allocation Log Hours
    {
      headerName: "Allocation Log Hours",
      field: "allocation_hours",
      headerTooltip: "Allocation Log Hours",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Capacity",
      headerTooltip: "Capacity",
      field: "capacity",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Job Chargeable Hours",
      headerTooltip: "Job Chargeable Hours",
      field: "job_chargeable_hours",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "job_chargeable_hours",
    },
    {
      headerName: "Job Non Chargeable Hours",
      headerTooltip: "Job Non Chargeable Hours",
      field: "job_non_chargeable_hours",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "job_non_chargeable_hours",
    },
  ];
  return columnDefs;
};
export const GetAgGridColumnDefForCost = () => {
  const columnDefs: any = [
    {
      headerName: "Date",
      field: "date",
      suppressMenu: true,
      tooltipField: "action",
    },
    {
      headerName: "Employee",
      headerTooltip: "Employee",
      field: "email_id",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Location",
      headerTooltip: "Location",
      field: "location",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Designation",
      headerTooltip: "Designation",
      field: "designation_name",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Business Unit",
      headerTooltip: "Business Unit",
      field: "business_unit",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    // {
    //   headerName: "Expertise",
    //   headerTooltip: "Expertise",
    //   field: "expertise",
    //   // flex: 0.8,
    //   filter: "agTextColumnFilter",
    //   filterParams: {
    //     suppressAndOrCondition: true,
    //   },
    //   sortable: true,
    //   unSortIcon: true,
    //   menuTabs: ["filterMenuTab"],
    //   tooltipField: "",
    // },
    // {
    //   headerName: "SMEG",
    //   headerTooltip: "SMEG",
    //   field: "sme_group_name",
    //   // flex: 0.8,
    //   filter: "agTextColumnFilter",
    //   filterParams: {
    //     suppressAndOrCondition: true,
    //   },
    //   sortable: true,
    //   unSortIcon: true,
    //   menuTabs: ["filterMenuTab"],
    //   tooltipField: "",
    // },
    // Actual Cost
    {
      headerName: "Actual Cost",
      field: "actual_cost",
      headerTooltip: "Actual Cost",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    // Allocated Cost
    {
      headerName: "Allocated Cost",
      field: "allocated_cost",
      headerTooltip: "Allocated Cost",
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Capacity Cost",
      headerTooltip: "Capacity Cost",
      field: "capacity_cost",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "",
    },
    {
      headerName: "Job Chargeable Cost",
      headerTooltip: "Job Chargeable Cost",
      field: "job_chargeable_cost",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "job_chargeable_cost",
    },
    {
      headerName: "Job Non Chargeable Cost ",
      headerTooltip: "Job Non Chargeable Cost",
      field: "job_non_chargeable_cost",
      // flex: 0.8,
      filter: "agTextColumnFilter",
      filterParams: {
        suppressAndOrCondition: true,
      },
      sortable: true,
      unSortIcon: true,
      menuTabs: ["filterMenuTab"],
      tooltipField: "job_non_chargeable_cost",
    },
  ];
  return columnDefs;
};
