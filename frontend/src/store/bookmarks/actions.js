import {
  SET_BOOKMARKS, UNSET_BOOKMARKS, ADD_BOOKMARK, EDIT_BOOKMARK, REMOVE_BOOKMARK
} from './types'

export function setBookmarks (context, bookmarks) {
  context.commit(SET_BOOKMARKS, bookmarks)
}

export function unsetBookmarks (context) {
  context.commit(UNSET_BOOKMARKS)
}

export function addBookmark (context, bookmark) {
  context.commit(ADD_BOOKMARK, bookmark)
}

export function editBookmark (context, bookmark) {
  context.commit(EDIT_BOOKMARK, bookmark)
}

export function removeBookmarks (context, bookmark) {
  context.commit(REMOVE_BOOKMARK, bookmark)
}
