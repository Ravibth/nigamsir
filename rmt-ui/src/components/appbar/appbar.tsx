import * as React from "react";
import {
  AppBar,
  Box,
  Toolbar,
  IconButton,
  MenuItem,
  Menu,
  Typography,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { NavLink, useLocation } from "react-router-dom";
import MoreIcon from "@mui/icons-material/MoreVert";
import LoggedInComp from "../logged-info/loggedIn";
import GrantLogoComp from "../grantLogo/grantlogo";
import * as constant from "./constant";
import { UserDetailsContext } from "../../contexts/userDetailsContext";
import SettingsSharpIcon from "@mui/icons-material/SettingsSharp";
import HelpMenu from "../help/help";
import NotificationPopover from "../notification/notification";
import MyTaskBar from "./mytaskbar/my-task-bar";
import {
  ITopNavMenuItem,
  getAdminSettings,
  getMenuByRoles,
  getNavigationPaths,
} from "./Util";
import "./../../../src/index.css";
import "./appbar.css";

function Appbarcomp() {
  const locationObj = useLocation();
  const userContext = React.useContext(UserDetailsContext);
  const [humbermenu, setHumbermenu] = React.useState("test");
  const [pagebackdrop, setBackdrop] = React.useState("backdrop");
  const [pagenavigation, setNavigation] = React.useState("page-nav");
  const [topNavMenuItems, setTopNavMenuItems] = React.useState<
    ITopNavMenuItem[]
  >([]);
  React.useEffect(() => {
    const payload: any[] = userContext.role; //["Admin"]
    getMenuByRoles(payload)
      .then((resp: any) => {
        userContext.setNavList(resp.data);
        const menu = resp.data.filter((i: any) => i.isDisplay === true);
        setTopNavMenuItems(menu);
      })
      .catch((err: any) => {});
  }, []);

  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(
    null
  );

  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [anchorEls, setAnchorEls] = React.useState([]);
  const [mobileMoreAnchorEl, setMobileMoreAnchorEl] =
    React.useState<null | HTMLElement>(null);
  const [adminSettingAnchorEl, setAdminSettingAnchorEl] =
    React.useState<null | HTMLElement>(null);

  const isMobileMenuOpen = Boolean(mobileMoreAnchorEl);
  const handleCloseNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(null);
  };
  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleMenuClick = (index, event) => {
    const newAnchorEls = [...anchorEls];
    newAnchorEls[index] = event.currentTarget;
    setAnchorEls(newAnchorEls);
  };
  const handleMenuClose = () => {
    setAnchorEls([]);
  };

  const handleAdminSettingMenuClick = (
    event: React.MouseEvent<HTMLElement>
  ) => {
    setAdminSettingAnchorEl(event.currentTarget);
  };
  const handleAdminSettingMenuClose = (
    event: React.MouseEvent<HTMLElement>
  ) => {
    setAdminSettingAnchorEl(null);
  };

  const handleMobileMenuClose = () => {
    setMobileMoreAnchorEl(null);
  };
  getAdminSettings(topNavMenuItems);

  const handleMobileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setMobileMoreAnchorEl(event.currentTarget);
  };

  const mobileMenuId = "primary-search-account-menu-mobile";
  const renderMobileMenu = (
    <Menu
      anchorEl={mobileMoreAnchorEl}
      anchorOrigin={{
        vertical: "top",
        horizontal: "right",
      }}
      id={mobileMenuId}
      keepMounted
      transformOrigin={{
        vertical: "top",
        horizontal: "right",
      }}
      open={isMobileMenuOpen}
      onClose={handleMobileMenuClose}
    >
      <MenuItem>
        <HelpMenu />
        <p>Help</p>
      </MenuItem>
      <MenuItem>
        <NotificationPopover />
        <p>Notification</p>
      </MenuItem>
    </Menu>
  );

  const gettopNavClassName = (currNode: ITopNavMenuItem) => {
    var className = "";
    if (currNode.path.toLowerCase() === locationObj.pathname.toLowerCase()) {
      className = "navitem-selected";
    } else if (
      currNode &&
      currNode.children &&
      currNode.children.length > 0 &&
      currNode.children.filter(
        (d) =>
          d.path.toLowerCase().trim() === locationObj.pathname.toLowerCase()
      ).length > 0
    ) {
      className = "navitem-selected";
    } else {
      className = "navitem-not-selected";
    }
    return className;
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  return (
    <Box className="header-main" sx={constant.AppBarMainBox}>
      <AppBar position="static" sx={constant.AppbarSxProps}>
        <Toolbar>
          <Box className="burgermenu" sx={constant.BoxOneSxProps}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              className="burger-menu"
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: "block", md: "none" },
              }}
            >
              {
                /* {isEmployee && */
                getNavigationPaths(topNavMenuItems).map(
                  (page: ITopNavMenuItem, index) => (
                    <NavLink to={page.path} key={index}>
                      <MenuItem onClick={handleCloseNavMenu}>
                        <Typography
                          textAlign="center"
                          color={"black important"}
                          // className={gettopNavClassName(page)}
                        >
                          {page.pageName}
                        </Typography>
                      </MenuItem>
                    </NavLink>
                  )
                )
              }
              {/* {!isEmployee &&
                requestorPage.map((page, index) => (
                  <NavLink to={page.pageLink} key={index}>
                    <MenuItem onClick={handleCloseNavMenu}>
                      <Typography textAlign="center">
                        {page.pageName}
                      </Typography>
                    </MenuItem>
                  </NavLink>
                ))} */}
            </Menu>
          </Box>
          <GrantLogoComp />
          <Box
            className={`page-navigation ${pagenavigation}`}
            sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}
          >
            <button
              className="close-nav"
              onClick={(e) => {
                setHumbermenu("test");
                setBackdrop("backdrop");
                setNavigation("page-nav");
              }}
            ></button>
            {
              /* {isEmployee && */
              getNavigationPaths(topNavMenuItems).map(
                (page: ITopNavMenuItem, index) => (
                  <NavLink
                    key={index}
                    to={
                      page && page.children && page.children.length > 0
                        ? null
                        : page.path
                    }
                    className={gettopNavClassName(page)}
                    style={({ isActive }) => {
                      return {
                        margin: "8px",
                        textDecoration: isActive ? "underline" : "none",
                        color: "white",
                      };
                    }}
                  >
                    {page.children && page.children.length > 0 ? (
                      <React.Fragment>
                        <Box
                          component="li"
                          key={index}
                          id={`basic-menu-${page.id}`}
                          onClick={(e) => handleMenuClick(index, e)}
                          sx={{
                            m: 2,
                            color: "white",
                            // display: "block",
                            textTransform: "initial",
                            fontSize: "15px",
                            display: "flex",
                            alignItems: "center",
                          }}
                          // endIcon={
                          //   <ArrowDropDownIcon
                          //     onClick={(e) => handleMenuClick(index, e)}
                          //   />
                          // }
                        >
                          {page.displayName}{" "}
                          {/* <ArrowDropDownIcon
                            onClick={(e) => handleMenuClick(index, e)}
                          /> */}
                        </Box>
                        <Menu
                          anchorEl={anchorEls[index]}
                          open={Boolean(anchorEls[index])}
                          onClose={handleMenuClose}
                        >
                          {page.children.map((child, childIndex) => (
                            <NavLink key={child.id} to={child.path}>
                              <MenuItem onClick={handleMenuClose}>
                                {child.displayName}
                              </MenuItem>
                            </NavLink>
                          ))}
                        </Menu>
                      </React.Fragment>
                    ) : (
                      <React.Fragment>
                        <Box
                          component="li"
                          key={index}
                          id={`basic-menu-${page.id}`}
                          onClick={handleClose}
                          sx={{
                            m: 2,
                            color: "white",
                            display: "flex",
                            textTransform: "initial",
                            fontSize: "15px",
                            alignItems: "center",
                          }}
                        >
                          {page.displayName}
                        </Box>
                      </React.Fragment>
                    )}
                  </NavLink>
                )
              )
            }
          </Box>
          <Box sx={{ flexGrow: 1 }} />
          <Box
            className="notification-container"
            sx={{ display: { xs: "none", md: "flex" } }}
          >
            {getAdminSettings(topNavMenuItems).length === 1 && (
              <>
                <IconButton onClick={handleAdminSettingMenuClick}>
                  <SettingsSharpIcon sx={{ color: "white" }} />
                </IconButton>
                <Menu
                  anchorEl={adminSettingAnchorEl}
                  open={Boolean(adminSettingAnchorEl)}
                  onClose={handleAdminSettingMenuClose}
                >
                  {getAdminSettings(topNavMenuItems)[0].children.map(
                    (child, childIndex) => (
                      <NavLink key={child.id} to={child.path}>
                        <MenuItem
                          onClick={handleAdminSettingMenuClose}
                          className="adminSettingsMenuItem"
                        >
                          {child.displayName}
                        </MenuItem>
                      </NavLink>
                    )
                  )}
                </Menu>
              </>
            )}

            <MyTaskBar />
            <NotificationPopover />

            {/* <HelpMenu /> */}
          </Box>
          <Box
            className="notification-dot"
            sx={{ display: { xs: "flex", md: "none" } }}
          >
            <IconButton
              size="large"
              aria-label="show more"
              aria-controls={mobileMenuId}
              aria-haspopup="true"
              onClick={handleMobileMenuOpen}
              color="inherit"
            >
              <MoreIcon />
            </IconButton>
          </Box>
          <div
            data-testid="burger"
            className={`burger-menu-toggle ${humbermenu}`}
            onClick={(e) => {
              setHumbermenu(humbermenu === "test" ? "active-burger" : "test");
              setNavigation(
                pagenavigation === "page-nav" ? "active-nav" : "page-nav"
              );
              setBackdrop(
                pagebackdrop === "backdrop" ? "active-backdrop" : "backdrop"
              );
            }}
          >
            <div></div>
            <div></div>
            <div></div>
          </div>
          <div
            className={`page-navigation-backdrop ${pagebackdrop}`}
            onClick={(e) => {
              setHumbermenu("test");
              setBackdrop("backdrop");
              setNavigation("page-nav");
            }}
          ></div>
          <LoggedInComp />
        </Toolbar>
      </AppBar>
      {renderMobileMenu}
      {/* {renderMenu} */}
    </Box>
  );
}
export default React.memo(Appbarcomp);
