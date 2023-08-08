// redux/tours.js

import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  tourData: [],
};

const toursSlice = createSlice({
  name: 'tours',
  initialState,
  reducers: {
    setTourData: (state, action) => {
      state.tourData = action.payload;
    },
  },
});

export const { setTourData } = toursSlice.actions;

export default toursSlice.reducer;
