import { SxProps } from "@mui/material/styles";
import { getDesignationList } from "../../services/configuration-services/configuration.service";

export const GetDesignationList = () => {
  return getDesignationList().then((response: any) => {
    let _json = [];
    if (response && response.data) {
      response.data.map((item: any) => {
        _json.push({
          //  label: `${item.designation_name}(${item.designation_id})`,
          label: `${item.designation_name}`,
          id: item.id,
          labelId: item.designation_id,
          isActive: true,
          grade: item.grade,
        });
      });
    }
    return _json;
  });
};

export const RequisitionButtons: SxProps = {
  display: "flex",
  justifyContent: "end",
};
