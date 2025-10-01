import { useForm } from "react-hook-form";
import {
  Box,
  Drawer,
  IconButton,
  Grid,
  ListItem,
  ListItemText,
  Tooltip,
  CardContent,
  Card,
  Typography,
} from "@mui/material";
import { EFilterType, IFilterParameters } from "./constant";
import CloseIcon from "@mui/icons-material/Close";
import ActionButton from "../../actionButton/actionButton";
import BackActionButton from "../../actionButton/backactionButton";
import ControllerAutoCompleteTextField from "../../controllerInputs/controllerAutoCompleteTextField";
import ControllerSlider from "../../controllerInputs/controllerSlider";
import { useEffect } from "react";

interface ISystemSuggestionsFilterForm {
  isFilterOpen: boolean;
  setOpenFilter: React.Dispatch<React.SetStateAction<boolean>>;
  filtersList: IFilterParameters[];
  filterValues: any;
  setFilterValues: React.Dispatch<any>;
}

const SystemSuggestionsFilterForm = (props: ISystemSuggestionsFilterForm) => {
  const { handleSubmit, control, setValue, reset, getValues } = useForm({
    mode: "onTouched",
  });

  const onSubmit = (data: any) => {
    props.setFilterValues(data);
    props.setOpenFilter(false);
  };

  const autoPopulateData = () => {
    if (props.filterValues) {
      Object.keys(props.filterValues).forEach((key: any) => {
        setValue(key, props.filterValues[key]);
      });
    }
  };

  useEffect(() => {
    if (props.isFilterOpen) {
      autoPopulateData();
    }
  }, [props.isFilterOpen, props.filtersList]);

  const returnFilterControl = (filterItem: IFilterParameters) => {
    switch (filterItem.filterType) {
      case EFilterType.Textfield:
        return (
          <ControllerAutoCompleteTextField
            name={filterItem.key}
            control={control}
            label={filterItem?.label}
            options={filterItem?.options || []}
            required={false}
            onChange={function (item: any): void {
              // setValue(filterItem.key, item.target.value);
            }}
          />
        );
      case EFilterType.Slider:
        return (
          <>
            <Typography
              id="input-slider"
              gutterBottom
            >
              {filterItem?.label}
            </Typography>
            <ControllerSlider
              name={filterItem.key}
              control={control}
              defaultMinValue={0}
              step={1}
              min={filterItem.min || 0}
              max={filterItem.max || 10}
              required={false}
              label={filterItem?.label}
              onChange={function (item: any): void {
                // setValue(filterItem.key, item.target.value);
              }}
            />
          </>
        );
      default:
        return "";
    }
  };

  return (
    <Box
      mt={2}
      ml={2}
      mr={2}
      mb={2}
    >
      <Drawer
        open={props.isFilterOpen}
        onClose={() => props.setOpenFilter(false)}
        sx={{ zIndex: 1300 }}
      >
        <form
          className="stacks"
          onSubmit={handleSubmit(onSubmit)}
        >
          <Grid
            container
            spacing={2}
          >
            <Grid
              item
              xs={12}
            >
              <ListItem>
                <ListItemText
                  className="filter-header-common"
                  primary="Filter By"
                />
                <IconButton
                  onClick={() => {
                    props.setOpenFilter(false);
                  }}
                >
                  <Tooltip title="close">
                    <CloseIcon />
                  </Tooltip>
                </IconButton>
              </ListItem>
            </Grid>
          </Grid>
          <Card
            style={{
              marginRight: "15px",
              marginLeft: "15px",
              border: "1px solid lightgray",
              borderRadius: "10px",
            }}
          >
            <CardContent>
              <Grid
                container
                spacing={2}
                sx={{ p: 1, maxWidth: "350px" }}
              >
                {props.filtersList
                  .filter((filterItem) => filterItem.isVisible)
                  .map((filterItem) => {
                    return (
                      <Grid
                        item
                        xs={12}
                        key={filterItem.key}
                      >
                        {returnFilterControl(filterItem)}
                      </Grid>
                    );
                  })}
              </Grid>
            </CardContent>
          </Card>
          <Grid
            container
            spacing={2}
            className="filter-btn-container"
            sx={{ marginBottom: 5 }}
          >
            <Grid
              item
              xs={4}
            >
              <ActionButton
                label={"Apply Filters"}
                onClick={function (e: any): void {}}
                disabled={false}
                type={"submit"}
                textTransform="none"
              />
            </Grid>
            <Grid
              item
              xs={1}
            ></Grid>
            <Grid
              item
              xs={4}
            >
              <BackActionButton
                label={"Reset"}
                textTransform="none"
                onClick={function (e: any): void {
                  reset();
                  onSubmit(getValues());
                }}
              />
            </Grid>
          </Grid>
        </form>
      </Drawer>
    </Box>
  );
};

export default SystemSuggestionsFilterForm;
