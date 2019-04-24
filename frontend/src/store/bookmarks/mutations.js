import {
  SET_BOOKMARKS, UNSET_BOOKMARKS, ADD_BOOKMARK, EDIT_BOOKMARK, REMOVE_BOOKMARK
} from './types'

export const mutations = {
  [SET_BOOKMARKS] (state, bookmarks) {
    state.bookmarks = [ ...bookmarks ]
  },
  [UNSET_BOOKMARKS] (state) {
    state.bookmark = []
  },
  [ADD_BOOKMARK] (state, bookmark) {
    state.bookmark.push({ ...bookmark })
  },
  [EDIT_BOOKMARK] (state, bookmark) {
    console.warn('TODO')
  },
  [REMOVE_BOOKMARK] (state, bookmark) {
    console.warn('TODO')
  }
}
