import { AgGridReact } from 'ag-grid-react';
import React from 'react'
import { defaultColDef } from './helper';

function AgGridTrainingAndEdu({data,columnDefs}) {
  return (
    <div className="ag-theme-alpine" style={{ height: 'auto', width: '100%' }}>
      <AgGridReact
        rowData={data}
        columnDefs={columnDefs}
        getRowStyle={() => ({ background: 'transparent' })}
        getRowId={params => params.data.id.toString()}
        defaultColDef={defaultColDef}
        suppressRowClickSelection
        rowSelection="multiple"
        domLayout='autoHeight'
      />
    </div>
  )
}

export default AgGridTrainingAndEdu;