import React, { useEffect, useState } from "react";
import { TreeView } from "@mui/x-tree-view/TreeView";
import { TreeItem } from "@mui/x-tree-view/TreeItem";
import "./treeview.css";

const TreeViewConfig = (props: any) => {
  return (
    <>
      <TreeView aria-label="file system navigator">
        <div className="config-container">
          <div className="bu-container">
            <TreeItem nodeId="1" label="Deals">
              <TreeItem nodeId="2" label="Due Diligence"></TreeItem>
              <TreeItem nodeId="3" label="Recovery & Reorganisation"></TreeItem>
            </TreeItem>
          </div>
          <div className="bu-container">
            <TreeItem nodeId="4" label="ESG & Risk Consulting">
              <TreeItem
                nodeId="5"
                label="Governance Risk & Operations (GRO)"
              ></TreeItem>
              <TreeItem nodeId="6" label="Cyber & IT Risk (Cyber)"></TreeItem>
            </TreeItem>
          </div>
        </div>
      </TreeView>
    </>
  );
};

export default TreeViewConfig;
