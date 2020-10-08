/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "../src/assets/js/pages/dashboard.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "../src/assets/js/pages/dashboard.js":
/*!*******************************************!*\
  !*** ../src/assets/js/pages/dashboard.js ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

var KCPage = function ()
{	
    return {
        init: function() 
		{
			startPage()
        }
    };
}();

// Class initialization
jQuery(document).ready(function() 
{
	$('#menuAdmin').show();

	/*
	//show export and admin menu if user is admin
	$.ajax({ 
		type: 'GET', 
		url: 'getSessionInfo?openpage',
		dataType: 'json',
		success: function (data) 
		{ 
			$('#txtUserName').html(data.user);
			lnk = getProfilePicture(getFMNO(data.userFullName));
			if (data.isAdmin=='yes')
			{
				$('#menuAdmin').show();
				$('#configLink').attr('href','configuration.html?open&id=' + data.configId)
			}
			else
			{				
				$('.adminReport').hide();
				$('#menuAdmin').remove()
			}
		}
	});	
	show notifications
	showNotifications();
	setInterval(function()	{ showNotifications() }, 60000);	
	loadReports();
	*/
	
    KCPage.init();
});

function loadReports()
{
	$.ajax({ 
		type: 'GET', 
		url: 'getReports?openpage&no-cache='+Math.floor(Math.random() * 10000000000),
		dataType: 'json',
		success: function (data)
		{ 

			$('#report1').attr('href',data.reports[0].url);
			$('#report2').attr('href',data.reports[1].url);
			$('#report3').attr('href',data.reports[2].url);
			$('#report4').attr('href',data.reports[3].url);
			$('#report5').attr('href',data.reports[4].url);
			$('#report6').attr('href',data.reports[5].url);
			$('#menuExport').show();
		}
	})	
}

function showNotifications()
{
	$.ajax({ 
		type: 'GET', 
		url: 'getNotificationsNotRead?openpage&no-cache='+Math.floor(Math.random() * 10000000000),
		dataType: 'json',
		success: function (data)
		{ 
			if (data.nr!='0')
			{
				$('#badgeNotifications').removeClass('kt-hidden');
				$('#badgeNotifications').html(data.nr);					
			}
			htm = '';
			$.each(data.notifications, function(i,e)
			{
				htm += '<a href="'+(e.id=='0'?'#':'notification.html?open&id='+e.id)+'" class="kt-notification__item"><div class="kt-notification__item-icon"><i class="flaticon-time-2"></i></div><div class="kt-notification__item-details"><div class="kt-notification__item-title">'+e.name+'</div></div></a>';
			});
			$('#lstNotifications').html(htm)
		}
	})
}

function getFMNO(fullname)
{
	fmno = '';
	var pattern = /^\d+$/;
	$.each(fullname.split(';'), function(i,e)
	{
		if (pattern.test(e)) 
		{
			fmno = e
		}
	});
	return fmno
}

function getProfilePicture(fmno)
{
	lnk = 'https://webassets.intranet.mckinsey.com/person/fmno_photos/'+fmno+'/profile.jpg';
	$('#userPic1').attr('src', lnk);
	$('#userPic2').attr('src', lnk);
	
	$("#userPic1").one("load", function() 
	{
		if (this.width==1)
		{
			$('#userPic1').attr('src', 'assets/media/logos/noprofile.jpg');
			$('#userPic2').attr('src', 'assets/media/logos/noprofile.jpg');
		}
	});
}

/***/ })

	/******/
});


function getUrlParameter(sParam)
{
	var sPageURL = window.location.search.substring(1),
		sURLVariables = sPageURL.split('&'),
		sParameterName,
		i;

	for (i = 0; i < sURLVariables.length; i++) {
		sParameterName = sURLVariables[i].split('=');

		if (sParameterName[0] === sParam) {
			return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
		}
	}
}

function unescapeData(data)
{
	if (data.hasOwnProperty('results')) {
		$.each(data.results, function (i, e) {
			data.results[i].id = unescape(e.id)
			data.results[i].text = unescape(e.text)
		})
	}
	else {
		jQuery.each(data, function (index, itemData) {
			itemData.id = unescape(itemData.id)
			itemData.text = unescape(itemData.text)
			itemData.proxy = unescape(itemData.proxy)
		})
	}

	return data
}