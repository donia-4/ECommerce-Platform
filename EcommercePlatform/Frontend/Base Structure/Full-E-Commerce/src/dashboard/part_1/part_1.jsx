import React, { use, useEffect, useState } from 'react';
     import "./css/Part_1.css"
 
import { ChartContainer } from '@mui/x-charts/ChartContainer';
import {
  LinePlot,
  MarkPlot,
  lineElementClasses,
  markElementClasses,
} from '@mui/x-charts/LineChart';
import { ChartsXAxis } from '@mui/x-charts/ChartsXAxis';
import { axisClasses } from '@mui/x-charts/ChartsAxis';
import { ChartsTooltipContainer, ChartsItemTooltipContent } from '@mui/x-charts/ChartsTooltip'; // Import tooltip components
import { ChartsReferenceLine } from '@mui/x-charts/ChartsReferenceLine'; // Import ChartsReferenceLine
import axios from 'axios';

export default function Part1()
{
const [pData,setPData]=useState([0]);
async function getPdata() {
  
  const {data} = await axios.get(import.meta.env.VITE_SalesOverMonths_BASE);
  
 setPData(data.value);  
 
 }
/* useEffect(()=>{
getPdata();

},[]) */


const xLabels = [
  'Apr',
  'May',
  'Jun',
  'Jul',
  'Aug',
  'Sep',
  'Oct',
  'Nov',
  'Dec'
];

function AvgPData()
{
  if(!pData.length==0)
{
  let sum = pData.reduce((a,b)=>a+b,0)
  return (sum/pData.length);
}
return 0;
}

const target = [Math.min(...pData),AvgPData(),Math.max(...pData)];



    return <div className="Part1-Dashboard">
<div className="header">
  <h6>Sales Overview</h6>
  <p>4% more in 2025</p>
</div>
  <ChartContainer
      width={550}
      height={300}
      series={[{ type: 'line', data: pData }]}
      xAxis={[{ scaleType: 'point', data: xLabels }]}
      yAxis={[{ label: 'Value' ,position:'none'}]}
      sx={{
        [`& .${lineElementClasses.root}`]: {
          stroke: '#8884d8',
          strokeWidth: 2,
        },
        [`& .${markElementClasses.root}`]: {
          stroke: '#8884d8',
          r: 4,
          fill: '#fff',
          strokeWidth: 2,
        },
      }}
    >
      <LinePlot />
      <MarkPlot />
      <ChartsXAxis
        sx={{
          [`& .${axisClasses.line}`]: {
            stroke: 'transparent', // Darker color for the axis line
            strokeWidth: 2, // Thicker line
          },
          [`& .${axisClasses.tick}`]: {
            stroke: 'transparent', // Darker color for ticks
            strokeWidth: 2, // Thicker ticks
          },
          [`& .${axisClasses.label}`]: {
            fill: '#333', // Darker color for labels
            fontWeight:'bold' ,
            fontSize:18
          },
        }}
      />
    
      <ChartsTooltipContainer slotProps={{ tooltip: { trigger: 'axis' } }}
   
      >
        <ChartsItemTooltipContent 
        sx={
          {

    fontStyle: 'italic',
    backgroundColor:'rgb(0,0,0,0.2)',
             

          }
        }
        
        />
      </ChartsTooltipContainer>
       {
        target.map((value ,index)=>{
          
        return <ChartsReferenceLine
        y={value} // Value on the Y-axis where the line should be
        lineStyle={{ stroke: 'hsla(0, 0%, 20%, 0.2)', strokeDasharray: '5 5' }} // Dashed red line
        key={index}
      />

        })
      } 
 
    </ChartContainer>    </div>;
}