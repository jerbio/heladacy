import { createSlice } from '@reduxjs/toolkit';

export const slice = createSlice({
  name: 'user',
  initialState: {
    username: null,
  },
  reducers: {
    updateUsername: (state, action) => {
      state.username = action.username;
    },
  },
});

export const { updateUsername } = slice.actions;

// The function below is called a selector and allows us to select a value from
// the state. Selectors can also be defined inline where they're used instead of
// in the slice file. For example: `useSelector((state) => state.counter.value)`
export const selectUsername = state => state.counter.value;

export default slice.reducer;
