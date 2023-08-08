// redux/store.js

import { configureStore } from '@reduxjs/toolkit';
import toursReducer from './toursSlice';

const store = configureStore({
  reducer: {
    tours: toursReducer,
  },
});

export default store;
