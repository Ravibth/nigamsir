/* eslint-disable no-lone-blocks */
/* eslint-disable array-callback-return */
/* eslint-disable @typescript-eslint/no-unused-vars */
import {
  Button,
  Drawer,
  List,
  Stack,
  ListItem,
  ListItemText,
  IconButton,
  Card,
  CardContent,
} from "@mui/material";
import React, { useEffect, useState } from "react";
import CloseIcon from "@mui/icons-material/Close";
import * as constant from "./../../../components/filter/project-filter/constant";
import ControllerCalendar from "../../controllerInputs/controlerCalendar";
import { useForm } from "react-hook-form";
import { GetAllMarketPlaceFilters } from "../../../services/marketPlace/get-all-project-for-marketplace";
import ControllerAutocompleteFilteredOptionsTextfield from "../../controllers/controller-autocomplete-filtered-options-textfield";
import "./marketplacefilter.css";
import MainFilter from "../../../common/images/MainFilter.png";
import { MarketfilterIconButton } from "../Heading/constant";
import {
  DefaultCommonMarketPlaceFillerControlValues,
  ICommonMarketPlaceFilterControl,
} from "../../common-allocation/common-allocation-filter/utils";
import { IMarketPlaceFilter } from "../marketplacetabs/marketplacetabs";

const Filtermarketplaceproject = (props: any) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    control,
    reset,
    getValues,
    setValue,
  } = useForm<ICommonMarketPlaceFilterControl>({
    mode: "onTouched",
    defaultValues: DefaultCommonMarketPlaceFillerControlValues,
  });

  const [openDrawerEmployee, setOpenDrawerEmployee] = useState(false);
  const [treeMasterList, setTreeMasterList] = useState([] as any);
  const [offeringsOptions, setOfferingsOptions] = useState([]);
  const [solutionsOptions, setSolutionsOptions] = useState([]);
  const [subIndustryOptions, setSubIndustryOptions] = useState([]);
  useEffect(() => {
    getFilterMasterData();
  }, []);

  const getFilterMasterData = async () => {
    var val = await GetAllMarketPlaceFilters();
    val["isAllocatedFiltervalue"] = ["Yes", "No"];
    setTreeMasterList(val);
  };

  function onSubmit(data: any) {
    props.setFilter({
      ...props.filter,
      ...data,
    });

    props.GetAllProjectDetailsForMarketPlaceData(
      1,
      props.myFilter.limit,
      data.showLiked ? data.showLiked : props?.myFilter?.showLiked,
      data.buFiltervalue,
      data.offeringsFiltervalue,
      data.solutionsFiltervalue,
      data.industryFiltervalue,
      data.subIndustryFiltervalue,
      data.locationFiltervalue,
      data.isAllocatedFiltervalue,
      data.startDateFiltervalue,
      data.endDateFiltervalue,
      data.sortColumn,
      data.sortOrder
    );
    //automatically close the filter drawer panel on button click
    setOpenDrawerEmployee(false);
  }

  var bulist = [];
  var offeringslist = [];
  var solutionslist = [];
  var industry = [];
  var subIndustry = [];
  var location = [];
  var isAllocatedvalue = [];
  bulist = treeMasterList.buFiltervalue;
  offeringslist = treeMasterList.offeringsFiltervalue;
  solutionslist = treeMasterList.solutionsFiltervalue;
  industry = treeMasterList.industryFiltervalue;
  subIndustry = treeMasterList.subIndustryFiltervalue;
  location = treeMasterList.locationFiltervalue;
  isAllocatedvalue = treeMasterList.isAllocatedFiltervalue;
  bulist = [...Array.from(new Set(bulist))];
  offeringslist = [...Array.from(new Set(offeringslist))];
  solutionslist = [...Array.from(new Set(solutionslist))];
  industry = [...Array.from(new Set(industry))];
  subIndustry = [...Array.from(new Set(subIndustry))];
  location = [...Array.from(new Set(location))];
  isAllocatedvalue = [...Array.from(new Set(isAllocatedvalue))];

  const CreateListofobj = (list: any): any => {
    var list_temp = [];
    for (var i = 0; i < list.length; i++) {
      list_temp.push({ title: list[i] });
    }
    return list_temp;
  };
  bulist = CreateListofobj(bulist);
  industry = CreateListofobj(industry);
  location = CreateListofobj(location);
  isAllocatedvalue = CreateListofobj(isAllocatedvalue);

  function resetClickHandler(): void {
    {
      let resetObj: IMarketPlaceFilter = {
        pagination: 1,
        limit: props.myFilter.limit,
        showLiked: props.myFilter.showLiked,
        buFiltervalue: [],
        offeringsFiltervalue: [],
        solutionsFiltervalue: [],
        industryFiltervalue: [],
        subIndustryFiltervalue: [],
        locationFiltervalue: [],
        isAllocatedFiltervalue: [],
        startDateFiltervalue: null,
        endDateFiltervalue: null,
        sortColumn: props.myFilter.sortColumn,
        sortOrder: props.myFilter.sortOrder,
      };

      let filterNewValues = {
        ...props.filter,
        ...resetObj,
      };

      reset(resetObj);

      Object.keys(getValues()).map((item) => {
        switch (item) {
          case "startDateFiltervalue":
            setValue(item, null);
            break;
          case "endDateFiltervalue":
            setValue(item, null);
            break;
          default:
            //setValue(item, []);
            break;
        }
      });

      props.GetAllProjectDetailsForMarketPlaceData(
        1,
        props.myFilter.limit,
        props.myFilter.showLiked,
        filterNewValues.buFiltervalue,
        filterNewValues.offeringsFiltervalue,
        filterNewValues.solutionsFiltervalue,
        filterNewValues.industryFiltervalue,
        filterNewValues.subIndustryFiltervalue,
        filterNewValues.locationFiltervalue,
        filterNewValues.isAllocatedFiltervalue,
        filterNewValues.startDateFiltervalue,
        filterNewValues.endDateFiltervalue,
        filterNewValues.sortColumn,
        filterNewValues.sortOrder
      );

      props.setFilter({
        ...props.filter,
        ...filterNewValues,
      });
    }
  }

  const onBUChanges = () => {
    setValue("offeringsFiltervalue", []);
    setValue("solutionsFiltervalue", []);
    setOfferingsOptions(
      getValues("buFiltervalue") && getValues("buFiltervalue").length > 0
        ? offeringslist?.filter((a) =>
            getValues("buFiltervalue").includes(a.bu)
          )
        : []
    );
  };
  const onOfferingsChanges = () => {
    setValue("solutionsFiltervalue", []);

    setSolutionsOptions(
      getValues("offeringsFiltervalue") &&
        getValues("offeringsFiltervalue").length > 0
        ? solutionslist?.filter((a) =>
            getValues("offeringsFiltervalue").includes(a.offerings)
          )
        : []
    );
  };

  return (
    <React.Fragment>
      <Drawer
        open={openDrawerEmployee}
        onClose={() => setOpenDrawerEmployee(false)}
        sx={constant.DrawerSxProps}
      >
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="stacks">
            <Stack
              direction="row"
              justifyContent="center"
              alignItems="center"
              spacing={2}
            >
              <List>
                <div>
                  <div>
                    <ListItem>
                      <ListItemText
                        className="filter-header"
                        primary="Filter By"
                      />
                      <IconButton
                        onClick={() => {
                          setOpenDrawerEmployee(false);
                        }}
                      >
                        <CloseIcon />
                      </IconButton>
                    </ListItem>
                    <Card
                      style={{
                        margin: "10px",
                        border: "1px solid lightgray",
                        borderRadius: "10px",
                      }}
                    >
                      <CardContent>
                        <ListItem className={"filter-control-container"}>
                          <ControllerAutocompleteFilteredOptionsTextfield
                            name="buFiltervalue"
                            control={control}
                            defaultValue={[]}
                            multiple={true}
                            sx={constant.AutocompleteSxProps}
                            options={bulist?.map((value) => value.title)}
                            onChange={onBUChanges}
                            label={"Business Unit"}
                          />
                        </ListItem>

                        <ListItem className={"filter-control-container"}>
                          <ControllerAutocompleteFilteredOptionsTextfield
                            {...register("offeringsFiltervalue")}
                            name="offeringsFiltervalue"
                            disabled={
                              getValues("buFiltervalue")?.length > 0
                                ? false
                                : true
                            }
                            control={control}
                            defaultValue={[]}
                            multiple={true}
                            sx={constant.AutocompleteSxProps}
                            options={offeringsOptions?.map((b) => b.name)}
                            onChange={onOfferingsChanges}
                            label={"Offerings"}
                          />
                        </ListItem>

                        <ListItem className={"filter-control-container"}>
                          <ControllerAutocompleteFilteredOptionsTextfield
                            name="solutionsFiltervalue"
                            disabled={
                              getValues("offeringsFiltervalue")?.length > 0
                                ? false
                                : true
                            }
                            control={control}
                            defaultValue={[]}
                            multiple={true}
                            sx={constant.AutocompleteSxProps}
                            options={solutionsOptions?.map((b) => b.name)}
                            onChange={() => {}}
                            label={"Solutions"}
                          />
                        </ListItem>

                        <ListItem className={"filter-control-container"}>
                          <ControllerAutocompleteFilteredOptionsTextfield
                            name="industryFiltervalue"
                            control={control}
                            defaultValue={[]}
                            multiple={true}
                            sx={constant.AutocompleteSxProps}
                            options={industry.map((value) => value.title)}
                            onChange={() => {
                              setValue("subIndustryFiltervalue", []);
                              setSubIndustryOptions(
                                getValues("industryFiltervalue") &&
                                  getValues("industryFiltervalue").length > 0
                                  ? subIndustry?.filter((a) =>
                                      getValues("industryFiltervalue").includes(
                                        a.industry
                                      )
                                    )
                                  : []
                              );
                            }}
                            label={"Industry"}
                          />
                        </ListItem>
                        <ListItem className={"filter-control-container"}>
                          <ControllerAutocompleteFilteredOptionsTextfield
                            name="subIndustryFiltervalue"
                            disabled={
                              getValues("industryFiltervalue")?.length > 0
                                ? false
                                : true
                            }
                            control={control}
                            defaultValue={[]}
                            multiple={true}
                            sx={constant.AutocompleteSxProps}
                            options={subIndustryOptions.map(
                              (value) => value.name
                            )}
                            onChange={(event, value) => {}}
                            label={"Sub-Industry"}
                          />
                        </ListItem>
                        <ListItem className={"filter-control-container"}>
                          <ControllerAutocompleteFilteredOptionsTextfield
                            name="locationFiltervalue"
                            control={control}
                            defaultValue={[]}
                            multiple={true}
                            sx={constant.AutocompleteSxProps}
                            options={location.map((value) => value.title)}
                            onChange={(event, value) => {}}
                            label={"Location"}
                          />
                        </ListItem>

                        <ListItem className={"filter-control-container"}>
                          <ControllerAutocompleteFilteredOptionsTextfield
                            name="isAllocatedFiltervalue"
                            control={control}
                            defaultValue={[]}
                            multiple={true}
                            sx={constant.AutocompleteSxProps}
                            options={isAllocatedvalue.map(
                              (value) => value.title
                            )}
                            onChange={(event, value) => {}}
                            label={"Is Allocated to Project"}
                          />
                        </ListItem>
                        <ListItem
                          className={
                            "filter-control-container calender-control"
                          }
                        >
                          <ControllerCalendar
                            name={`startDateFiltervalue`}
                            control={control}
                            label={"Start Date"}
                            error={errors.startDateFiltervalue ? true : false}
                            required={false}
                            onChange={(date: any) => {
                              props.setFilter({
                                ...props.filter,
                                startDateFiltervalue: date,
                              });
                            }}
                            defaultValue={props.filter.startDateFiltervalue}
                            maxDate={""}
                          />
                        </ListItem>
                        <ListItem className="filter-control-container">
                          <ControllerCalendar
                            name={`endDateFiltervalue`}
                            control={control}
                            label={"End Date"}
                            error={errors.endDateFiltervalue ? true : false}
                            required={false}
                            onChange={(date: any) => {
                              props.setFilter({
                                ...props.filter,
                                endDateFiltervalue: date,
                              });
                            }}
                            defaultValue={props.filter.endDateFiltervalue}
                            maxDate={""}
                          />
                        </ListItem>
                      </CardContent>
                    </Card>
                  </div>
                  <div className="filter-btn-container">
                    <Button
                      type="submit"
                      className="rmt-action-button"
                      variant="contained"
                      sx={constant.ApplyFilterButtonSxProps}
                    >
                      Apply Filters
                    </Button>
                    <Button
                      sx={constant.CloseButtonSxProps}
                      variant="outlined"
                      onClick={resetClickHandler}
                    >
                      Reset
                    </Button>
                  </div>
                </div>
              </List>
            </Stack>
          </div>
        </form>
      </Drawer>
      <Button
        onClick={() => setOpenDrawerEmployee(!openDrawerEmployee)}
        variant="outlined"
        sx={MarketfilterIconButton}
      >
        <img src={MainFilter} alt="upload" />
      </Button>
    </React.Fragment>
  );
};

export default Filtermarketplaceproject;
