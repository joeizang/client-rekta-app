import { Grid, Paper } from '@material-ui/core'
import React, { FC, Fragment } from 'react'
import Chart from '../../components/Chart'
import Deposits from '../../components/Deposits'
import Orders from '../../components/Orders'
import { DashboardProps } from '../../types/supplierTypes'

export const SalesDashboard: FC<DashboardProps> = ({
                                                       fixedHeightPaper,
                                                       classes,
                                                   }) => {
    return (
        <Fragment>
            <Grid container spacing={3}>
                {/* Chart */}
                <Grid item xs={12} md={8} lg={9}>
                    <Paper className={fixedHeightPaper}>
                        <Chart />
                    </Paper>
                </Grid>
                {/* Recent Deposits */}
                <Grid item xs={12} md={4} lg={3}>
                    <Paper className={fixedHeightPaper}>
                        <Deposits />
                    </Paper>
                </Grid>
                {/* Recent Orders */}
                <Grid item xs={12}>
                    <Paper
                        style={{
                            padding: 16,
                            display: 'flex',
                            overflow: 'auto',
                            flexDirection: 'column',
                        }}
                    >
                        <Orders />
                    </Paper>
                </Grid>
            </Grid>
        </Fragment>
    )
}
