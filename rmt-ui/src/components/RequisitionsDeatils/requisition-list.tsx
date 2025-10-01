import * as React from "react";
import TextField from "@mui/material/TextField";
import Autocomplete from "@mui/material/Autocomplete";
import { useEffect, useState } from "react";
import moment from "moment";
import { getHoursLabel } from "../../global/utils";
import ControllerAutocompleteFilteredOptionsTextfield from "../controllers/controller-autocomplete-filtered-options-textfield";
import { GlobalConfigs } from "../../global/constant";

export default function RequisitionList(props: any) {
  const { requisitions } = props;
  const [value, setValue] = useState<any>([]);
  const [requestionsList, setRequestionsList] = useState([]);
  const [inputValue, setInputValue] = React.useState("");
  useEffect(() => {
    const req: any = [];
    if (requisitions.length > 0) {
      let count = 1;
      requisitions?.forEach((data: any) => {
        req.push({
          label: `${count} - ${data.designation} | ${moment(
            data.startDate
          ).format(GlobalConfigs.dateFormat)} | ${moment(data.endDate).format(
            GlobalConfigs.dateFormat
          )} | ${data.totalHours} ${getHoursLabel(data.totalHours)}`,
          id: data.id,
        });
        count++;
      });
    }
    if (req.length) {
      props.onRequisitionsChange(req[0]);
    } else {
      setRequestionsList(req);
    }
    setRequestionsList(req);
    setValue(req[0]);
  }, [requisitions]);

  return (
    <>
      {" "}
      {requestionsList.length == 1 ? (
        <TextField
          disabled
          label={"Requisition"}
          sx={{ width: "100%" }}
          value={requestionsList.map((item: any) => item?.label).join(", ")}
        />
      ) : (
        <Autocomplete
          disablePortal
          id="combo-box-demo"
          options={requestionsList}
          value={value}
          getOptionLabel={(option) => option?.label}
          sx={{ width: "100%" }}
          onChange={(e, data) => {
            setValue(data);
            props.onRequisitionsChange(data);
          }}
          //  onChange={(e, data) => props.onRequisitionsChange(data)}
          renderInput={(params) => (
            <TextField
              {...params}
              label="Select Requisition"
            />
          )}
        />
      )}
    </>
  );
}
