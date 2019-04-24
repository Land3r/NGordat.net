const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'home',
        component: () => import('pages/Index.vue')
      }
    ]
  },
  {
    path: '/login',
    component: () => import('layouts/EmptyLayout.vue'),
    children: [
      {
        path: '',
        name: 'loginpage',
        component: () => import('pages/NotLoggedIn/Login.vue')
      }
    ]
  },
  {
    path: '/createaccount',
    component: () => import('layouts/EmptyLayout.vue'),
    children: [
      {
        path: '',
        name: 'createaccountpage',
        component: () => import('pages/NotLoggedIn/CreateAccount.vue')
      }
    ]
  },
  {
    path: '/lostpassword',
    component: () => import('layouts/EmptyLayout.vue'),
    children: [
      {
        path: '',
        name: 'lostpasswordpage',
        component: () => import('pages/NotLoggedIn/LostPassword.vue')
      }
    ]
  }
]

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
